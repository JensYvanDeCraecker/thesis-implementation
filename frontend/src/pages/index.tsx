import { ParcelInfo, StateInfo } from '@/interfaces';
import { Button } from '@/ui/components/Button';
import { FindParcel } from '@/ui/components/FindParcel';
import { Parcel } from '@/ui/components/Parcel';
import { UpdateState } from '@/ui/components/UpdateState';
import { api } from '@/utils/axios';
import { useEffect, useState } from 'react';
import Layout from '../components/Layout';

const IndexPage = () => {
    const [parcel, setParcel] = useState<ParcelInfo>();
    const [states, setStates] = useState<StateInfo[]>();

    useEffect(() => {
        getStates().then(states => setStates(states));
    }, []);

    const handleSearch = (parcelId: string) => {
        getParcel(parcelId).then(parcel => setParcel(parcel));
    };

    const handleUpdate = (stateId: number) => {
        if (!parcel) return;
        updateState(parcel.id, stateId).then(parcel => setParcel(parcel));
    };

    const handleGenerate = () => {
        generate().then(parcelId => handleSearch(parcelId));
    };

    return (
        <Layout title="Parcel tracker">
            <div className="c-index">
                <div className="c-index__controls">
                    <h1>Parcel tracker</h1>
                    <h2>Search your parcel</h2>
                    <FindParcel onSearch={handleSearch} />
                    {parcel ? (
                        <>
                            <h2>Update state</h2>
                            <UpdateState states={states} onUpdate={handleUpdate} />
                        </>
                    ) : null}
                    <h2>Generate parcel (demo)</h2>
                    <Button text="Generate" onClick={handleGenerate}></Button>
                </div>
                <Parcel parcel={parcel} />
            </div>
        </Layout>
    );
};

export default IndexPage;

async function getStates() {
    const { data } = await api.get<StateInfo[]>('/api/states');
    return data;
}

async function getParcel(id: string) {
    try {
        const { data } = await api.get<ParcelInfo>('/api/parcels/' + id);
        return data;
    } catch (_) {
        return;
    }
}

async function updateState(parcelId: string, stateId: number) {
    const { data } = await api.post<ParcelInfo>(`/api/parcels/${parcelId}/state/${stateId}`);
    return data;
}

async function generate() {
    const { data } = await api.post<[string]>('/api/generate/1');
    return data[0];
}
