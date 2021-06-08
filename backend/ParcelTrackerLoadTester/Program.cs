using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Http;
using NBomber.Plugins.Http.CSharp;

namespace ParcelTrackerLoadTester
{
    internal class Program
    {
        private static readonly IClientFactory<HttpClient> clientFactory = HttpClientFactory.Create();
        private static string server = "";

        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddEnvironmentVariables().AddCommandLine(args).Build();
            server = configuration["Host"];
            Scenario getAllStatesScenario = CreateAllStatesScenario();
            Scenario getParcelsScenario = CreateGetParcelsScenario();
            Scenario getParcelLoadTestScenario = CreateParcelLoadTestScenario();
            NBomberRunner.RegisterScenarios(getAllStatesScenario).WithReportFileName("without_caching_all_states").Run();
            //NBomberRunner.RegisterScenarios(getParcelsScenario).WithReportFileName("without_caching_get_parcels").Run();
            //NBomberRunner.RegisterScenarios(getParcelLoadTestScenario).WithReportFileName("without_caching_parcels_load").Run();
        }

        private static Scenario CreateAllStatesScenario()
        {
            IStep getAllStates = Step.Create("get_all_states", clientFactory, ctx =>
            {
                HttpRequest request = Http.CreateRequest("GET", server + "/api/states");
                return Http.Send(request, ctx);
            }, TimeSpan.FromMinutes(2));
            return ScenarioBuilder.CreateScenario("get_all_states", getAllStates)
                .WithLoadSimulations(LoadSimulation.NewKeepConstant(100, TimeSpan.FromMinutes(10))).WithoutWarmUp();
        }

        private static Scenario CreateGetParcelsScenario()
        {
            List<string> generatedParcels = new();
            IFeed<string> parcelFeed = Feed.CreateCircular("parcel_feed", generatedParcels);
            return ScenarioBuilder.CreateScenario("get_parcels", CreateGetParcelStep("get_parcel_1", parcelFeed)).WithInit(async ctx =>
            {
                IEnumerable<string> parcels = await GenerateParcelsAsync(5000, ctx.CancellationToken);
                generatedParcels.AddRange(parcels ?? Enumerable.Empty<string>());
            }).WithLoadSimulations(LoadSimulation.NewKeepConstant(100, TimeSpan.FromMinutes(10))).WithoutWarmUp();
        }

        private static Scenario CreateParcelLoadTestScenario()
        {
            List<string> generatedParcels = new();
            IFeed<string> parcelFeed = Feed.CreateCircular("parcel_feed", generatedParcels);
            IStep[] parcelLoadTestSteps = { CreateGetParcelStep("get_parcel_1", parcelFeed), CreateUpdateStateStep("set_parcel_state_sorting", 3), CreateGetParcelStep("get_parcel_2"), };
            return ScenarioBuilder.CreateScenario("parcels_get_update_get", parcelLoadTestSteps).WithInit(async ctx =>
            {
                IEnumerable<string> parcels = await GenerateParcelsAsync(5000, ctx.CancellationToken);
                generatedParcels.AddRange(parcels ?? Enumerable.Empty<string>());
            }).WithLoadSimulations(LoadSimulation.NewKeepConstant(100, TimeSpan.FromMinutes(10))).WithoutWarmUp();
        }

        private static IStep CreateUpdateStateStep(string name, int stateId)
        {
            return Step.Create(name, clientFactory, async ctx =>
            {
                string parcelId = ctx.GetPreviousStepResponse<string>();
                HttpRequest request = Http.CreateRequest("POST", server + $"/api/parcels/{parcelId}/state/{stateId}");
                Response response = await Http.Send(request, ctx);
                return response.IsError ? response : Response.Ok(parcelId, response.StatusCode, response.SizeBytes, response.LatencyMs);
            }, TimeSpan.FromMinutes(2));
        }

        private static IStep CreateGetParcelStep(string name, IFeed<string> feeder = null)
        {
            async Task<Response> Execute<T>(IStepContext<HttpClient, T> ctx)
            {
                string parcelId = feeder != null ? ctx.FeedItem as string : ctx.GetPreviousStepResponse<string>();
                HttpRequest request = Http.CreateRequest("GET", server + $"/api/parcels/{parcelId}");
                Response response = await Http.Send(request, ctx);
                return response.IsError ? response : Response.Ok(parcelId, response.StatusCode, response.SizeBytes, response.LatencyMs);
            }

            return feeder != null ? Step.Create(name, clientFactory, feeder, Execute, TimeSpan.FromMinutes(2)) : Step.Create(name, clientFactory, Execute, TimeSpan.FromMinutes(2));
        }

        private static async Task<IEnumerable<string>> GenerateParcelsAsync(int amount, CancellationToken cancellationToken)
        {
            using HttpClient client = new();
            HttpResponseMessage response = await client.PostAsync(server + "/api/generate/" + amount, new ByteArrayContent(new byte[0], 0, 0), cancellationToken);
            byte[] data = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            return JsonSerializer.Deserialize<IEnumerable<string>>(data);
        }
    }
}