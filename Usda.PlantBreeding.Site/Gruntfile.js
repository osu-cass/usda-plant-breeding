/// <binding BeforeBuild='webpack:prod' />
'use strict';
const webpackConfig = require('./webpack.config');


module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["wwwroot/scripts/*", "temp/"],

        // TODO: Minify JS eventually
        //uglify: {
        //    all: {
        //        src: ["wwwroot/scripts/*.js"],
        //        dest: "wwwroot/scripts/*.min.js"
        //    }
        //},

        //cssmin: {
        //    options: {
        //        shorthandCompacting: false,
        //        roundingPrecision: -1
        //    },
        //    target: {
        //        files: {
        //            "wwwroot/css/site.min.css": ["wwwroot/css/site.css"]
        //        }
        //    }
        //},

        ts: {
            default: {
                tsconfig: {
                    tsconfig: 'Scripts/typescript/tsconfig.json',
                },
            },
        },

        watch: {
            files: ["Scripts/typescript/*"],
            tasks: ["tsrecompile"]
        },
        
        karma: {
            unit: {
                configFile: 'Scripts/karma.conf.js'
            }
        },
        webpack: {
              options: {
              },
              prod: webpackConfig
            }

    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    //grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-ts');
    grunt.loadNpmTasks('grunt-webpack');

    //grunt.registerTask("all", ['clean', 'ts', 'cssmin']); //,'uglify']); // TODO: Minify JS eventually
    grunt.registerTask("tsrecompile", ['clean', 'webpack:prod']);


};