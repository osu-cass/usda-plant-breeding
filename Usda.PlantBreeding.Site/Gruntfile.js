/// <binding BeforeBuild='webpack:prod' AfterBuild='copy' />
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
        },

        copy: {
            noty: {
                files: [
                    {
                        src: "node_modules/noty/js/noty/jquery.noty.js", dest: "Scripts/noty/jquery.noty.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/layouts/top.js", dest: "Scripts/noty/top.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/layouts/center.js", dest: "Scripts/noty/center.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/themes/default.js", dest: "Scripts/noty/default.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/themes/bootstrap.js", dest: "Scripts/noty/bootstrap.js"
                    },
                    {
                        src: "node_modules/bootstrap-multiselect/dist/js/bootstrap-multiselect.js", dest: "Scripts/noty/bootstrap-multiselect.js"
                    }
                ]
            },
            jqueryajax: {
                files: [
                    {
                        src: "node_modules/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.js", dest: "Scripts/jquery-ajax/jquery.unobtrusive-ajax.js"
                    }
                ]
            },
            jquery: {
                files: [
                    {
                        src: "node_modules/jquery/dist/jquery.js", dest: "Scripts/jquery/jquery.js"
                    },
                    {
                        src: "node_modules/jquery/external/sizzle/dist/sizzle.js", dest: "Scripts/jquery/sizzle.js"
                    }
                ]
            },
            jqueryval: {
                files: [
                    {
                        src: "node_modules/jquery-validation/dist/jquery.validate.js", dest: "Scripts/jquery-validation/jquery.validate.js"
                    },
                    {
                        src: "node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js", dest: "Scripts/jquery-validation/jquery.validate.unobtrusive.js"
                    }
                ]
            },
            bootstrap: {
                files: [
                    {
                        src: "node_modules/bootstrap/dist/js/bootstrap.js", dest: "Scripts/bootstrap/bootstrap.js"
                    },
                    {
                        src: "node_modules/respond.js/src/respond.js", dest: "Scripts/bootstrap/respond.js"
                    }
                ]
            },
            jqueryui: {
                files: [
                    {
                        src: "node_modules/jquery-ui/ui/*.js", dest: "Scripts/jquery-ui/"
                    },
                    {
                        src: "node_modules/jquery-ui/ui/effects/*.js", dest: "Scripts/jquery-ui/"
                    },
                    {
                        src: "node_modules/jquery-ui/ui/widgets/*.js", dest: "Scripts/jquery-ui/"
                    }
                ]
            },
            offlinejs: {
                files: [
                    {
                        src: "node_modules/offline-js/offline.min.js", dest: "Scripts/offline-js/offline.min.js"
                    }
                ]
            },
            bootstrapstyle: {
                files: [
                    {
                        src: "node_modules/bootstrap/dist/css/bootstrap.min.css", dest: "Stylesheets/bootstrap/bootstrap.min.css"
                    }
                ]
            },
            bootstrapglyph: {
                files: [
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.eot", dest: "Stylesheets/fonts/glyphicons-halflings-regular.eot"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.svg", dest: "Stylesheets/fonts/glyphicons-halflings-regular.svg"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.ttf", dest: "Stylesheets/fonts/glyphicons-halflings-regular.ttf"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.woff", dest: "Stylesheets/fonts/glyphicons-halflings-regular.woff"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.woff2", dest: "Stylesheets/fonts/glyphicons-halflings-regular.woff2"
                    }
                ]
            },
            basestyle: {
                files: [
                    {
                        src: "node_modules/jquery-ui/themes/base/core.css", dest: "Stylesheets/base/core.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/resizable.css", dest: "Stylesheets/base/resizable.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/selectable.css", dest: "Stylesheets/base/selectable.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/accordion.css", dest: "Stylesheets/base/accordion.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/autocomplete.css", dest: "Stylesheets/base/autocomplete.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/button.css", dest: "Stylesheets/base/button.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/dialog.css", dest: "Stylesheets/base/dialog.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/slider.css", dest: "Stylesheets/base/slider.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/tabs.css", dest: "Stylesheets/base/tabs.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/datepicker.css", dest: "Stylesheets/base/datepicker.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/progressbar.css", dest: "Stylesheets/base/progressbar.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/theme.css", dest: "Stylesheets/base/theme.css"
                    },
                    {
                        src: "node_modules/js-autocomplete/auto-complete.css", dest: "Stylesheets/base/auto-complete.css"
                    }
                ]
            },
            offlinestyle: {
                files: [
                    {
                        src: "node_modules/offline-js/themes/offline-theme-slide.css", dest: "Stylesheets/offline/offline-theme-slide.css"
                    },
                    {
                        src: "node_modules/offline-js/themes/offline-language-english.css", dest: "Stylesheets/offline/offline-language-english.css"
                    },
                    {
                        src: "node_modules/offline-js/themes/offline-theme-slide-indicator.css", dest: "Stylesheets/offline/offline-theme-slide-indicator.css"
                    },
                    {
                        src: "node_modules/offline-js/themes/offline-language-english-indicator.css", dest: "Stylesheets/offline/offline-language-english-indicator.css"
                    }
                ]
            },
            table: {
                files: [
                    {
                        src: "node_modules/floatthead/dist/jquery.floatThead.min.js", dest: "Scripts/table/jquery.floatThead.min.js"
                    },
                    {
                        src: "node_modules/tablesorter/dist/js/jquery.tablesorter.min.js", dest: "Scripts/table/jquery.tablesorter.min.js"
                    }
                ]
            },
            datatable: {
                files: [
                    { src: "node_modules/datatables.net/js/jquery.dataTables.min.js", dest: "Scripts/datatable/jquery.dataTables.min.js" },
                    { src: "node_modules/datatables.net-bs/js/dataTables.bootstrap.min.js", dest: "Scripts/datatable/dataTables.bootstrap.min.js" },
                    { src: "node_modules/datatables.net-fixedcolumns/js/dataTables.fixedColumns.min.js", dest: "Scripts/datatable/dataTables.fixedColumns.min.js" },
                    { src: "node_modules/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js", dest: "Scripts/datatable/dataTables.fixedHeader.min.js" }
                ]
            },
            datatablestyle: {
                files: [
                    {
                        src: "node_modules/datatables.net-bs/css/dataTables.bootstrap.min.css", dest: "Stylesheets/datatable/dataTables.bootstrap.min.css"
                    },
                    {
                        src: "node_modules/datatables.net-fixedcolumns-bs/css/fixedColumns.bootstrap.min.css", dest: "Stylesheets/datatable/fixedColumns.bootstrap.min.css"
                    },
                    {
                        src: "node_modules/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css", dest: "Stylesheets/datatable/fixedHeader.bootstrap.min.css"
                    }
                ]
            },
            fontawesome: {
                files: [
                    { src: "node_modules/font-awesome/css/font-awesome.min.css", dest: "Stylesheets/fontawesome/font-awesome.min.css" }
                ]
            }

        }

    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    //grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-ts');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-webpack');

    //grunt.registerTask("all", ['clean', 'ts', 'cssmin']); //,'uglify']); // TODO: Minify JS eventually
    grunt.registerTask("tsrecompile", ['clean', 'webpack:prod']);


};