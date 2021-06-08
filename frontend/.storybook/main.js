const tspaths = require('tsconfig-paths-webpack-plugin');

module.exports = {
    stories: ['../src/ui/**/*.stories.@(js|jsx|ts|tsx)'],
    addons: ['@storybook/addon-links', '@storybook/addon-essentials', '@storybook/preset-scss'],
    webpackFinal: config => {
        config.resolve.plugins ? config.resolve.plugins.push(new tspaths()) : (config.resolve.plugins = [new tspaths()]);
        let rule = config.module.rules.find(r => r.test && r.test.toString().includes('svg') && r.loader && r.loader.includes('file-loader'));
        rule.test = /\.(ico|jpg|jpeg|png|gif|eot|otf|webp|ttf|woff|woff2|cur|ani)(\?.*)?$/;
        config.module.rules.push({
            test: /\.svg$/,
            use: ['@svgr/webpack']
        });
        return config;
    }
};
