var path = require('path');

module.exports = {
    entry: {
          "usda.phenotype-page": "./Scripts/typescript/PhenotypeEntry.tsx",
          "usda.order-page": "./Scripts/typescript/OrderPage.tsx",
          "polyfill": "./Scripts/typescript/Polyfill.ts",
          "usda.create-distribution-page": "./Scripts/typescript/CreateDistributionPage.tsx",
          "usda.location-page" : "./Scripts/typescript/LocationPage.tsx"
    },

    output: {
        path: path.join(__dirname, 'Scripts/js/'),
        filename: "[name].js",
        libraryTarget: "var",
        library: "EntryPoint"
    },

    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",

    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: [".ts", ".tsx", ".js", ".json"]
    },

    module: {
        rules: [
            // All files with a '.ts' or '.tsx' extension will be handled by 'awesome-typescript-loader'.
            { test: /\.tsx?$/, loader: "awesome-typescript-loader", options: { configFileName: path.join(__dirname, 'Scripts/typescript/tsconfig.json')} },

            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { enforce: "pre", test: /\.js$/, loader: "source-map-loader" }
        ]
    },

    externals: {
        "react": "React",
        "react-dom": "ReactDOM",
        "jquery": "jQuery"
    },
};