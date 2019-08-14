/// <binding BeforeBuild='webpack:prod, copy' />
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
                        src: "node_modules/noty/js/noty/jquery.noty.js", dest: "Scripts/Lib/noty/jquery.noty.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/layouts/top.js", dest: "Scripts/Lib/noty/top.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/layouts/center.js", dest: "Scripts/Lib/noty/center.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/themes/default.js", dest: "Scripts/Lib/noty/default.js"
                    },
                    {
                        src: "node_modules/noty/js/noty/themes/bootstrap.js", dest: "Scripts/Lib/noty/bootstrap.js"
                    },
                    {
                        src: "node_modules/bootstrap-multiselect/dist/js/bootstrap-multiselect.js", dest: "Scripts/Lib/noty/bootstrap-multiselect.js"
                    }
                ]
            },
            jqueryajax: {
                files: [
                    {
                        src: "node_modules/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.js", dest: "Scripts/Lib/jquery-ajax/jquery.unobtrusive-ajax.js"
                    }
                ]
            },
            jquery: {
                files: [
                    {
                        src: "node_modules/jquery/dist/jquery.js", dest: "Scripts/Lib/jquery/jquery.js"
                    },
                    {
                        src: "node_modules/jquery/external/sizzle/dist/sizzle.js", dest: "Scripts/Lib/jquery/sizzle.js"
                    }
                ]
            },
            jqueryval: {
                files: [
                    {
                        src: "node_modules/jquery-validation/dist/jquery.validate.js", dest: "Scripts/Lib/jquery-validation/jquery.validate.js"
                    },
                    {
                        src: "node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js", dest: "Scripts/Lib/jquery-validation/jquery.validate.unobtrusive.js"
                    }
                ]
            },
            bootstrap: {
                files: [
                    {
                        src: "node_modules/bootstrap/dist/js/bootstrap.js", dest: "Scripts/Lib/bootstrap/bootstrap.js"
                    },
                    {
                        src: "node_modules/respond.js/src/respond.js", dest: "Scripts/Lib/bootstrap/respond.js"
                    }
                ]
            },
            jqueryui: {
                files: [
                    {
                        src: "node_modules/jquery-ui/ui/*.js", dest: "Scripts/Lib/jquery-ui/"
                    },
                    {
                        src: "node_modules/jquery-ui/ui/effects/*.js", dest: "Scripts/Lib/jquery-ui/"
                    },
                    {
                        src: "node_modules/jquery-ui/ui/widgets/*.js", dest: "Scripts/Lib/jquery-ui/"
                    }
                ]
            },
            offlinejs: {
                files: [
                    {
                        src: "node_modules/offline-js/offline.min.js", dest: "Scripts/Lib/offline-js/offline.min.js"
                    }
                ]
            },
            bootstrapstyle: {
                files: [
                    {
                        src: "node_modules/bootstrap/dist/css/bootstrap.min.css", dest: "Stylesheets/Lib/bootstrap/bootstrap.min.css"
                    }
                ]
            },
            bootstrapglyph: {
                files: [
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.eot", dest: "Stylesheets/Lib/fonts/glyphicons-halflings-regular.eot"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.svg", dest: "Stylesheets/Lib/fonts/glyphicons-halflings-regular.svg"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.ttf", dest: "Stylesheets/Lib/fonts/glyphicons-halflings-regular.ttf"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.woff", dest: "Stylesheets/Lib/fonts/glyphicons-halflings-regular.woff"
                    },
                    {
                        src: "node_modules/bootstrap/dist/fonts/glyphicons-halflings-regular.woff2", dest: "Stylesheets/Lib/fonts/glyphicons-halflings-regular.woff2"
                    }
                ]
            },
            basestyle: {
                files: [
                    {
                        src: "node_modules/jquery-ui/themes/base/core.css", dest: "Stylesheets/Lib/base/core.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/resizable.css", dest: "Stylesheets/Lib/base/resizable.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/selectable.css", dest: "Stylesheets/Lib/base/selectable.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/accordion.css", dest: "Stylesheets/Lib/base/accordion.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/autocomplete.css", dest: "Stylesheets/Lib/base/autocomplete.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/button.css", dest: "Stylesheets/Lib/base/button.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/dialog.css", dest: "Stylesheets/Lib/base/dialog.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/slider.css", dest: "Stylesheets/Lib/base/slider.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/tabs.css", dest: "Stylesheets/Lib/base/tabs.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/datepicker.css", dest: "Stylesheets/Lib/base/datepicker.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/progressbar.css", dest: "Stylesheets/Lib/base/progressbar.css"
                    },
                    {
                        src: "node_modules/jquery-ui/themes/base/theme.css", dest: "Stylesheets/Lib/base/theme.css"
                    },
                    {
                        src: "node_modules/js-autocomplete/auto-complete.css", dest: "Stylesheets/Lib/base/auto-complete.css"
                    }
                ]
            },
            offlinestyle: {
                files: [
                    {
                        src: "node_modules/offline-js/themes/offline-theme-slide.css", dest: "Stylesheets/Lib/offline/offline-theme-slide.css"
                    },
                    {
                        src: "node_modules/offline-js/themes/offline-language-english.css", dest: "Stylesheets/Lib/offline/offline-language-english.css"
                    },
                    {
                        src: "node_modules/offline-js/themes/offline-theme-slide-indicator.css", dest: "Stylesheets/Lib/offline/offline-theme-slide-indicator.css"
                    },
                    {
                        src: "node_modules/offline-js/themes/offline-language-english-indicator.css", dest: "Stylesheets/Lib/offline/offline-language-english-indicator.css"
                    }
                ]
            },
            table: {
                files: [
                    {
                        src: "node_modules/floatthead/dist/jquery.floatThead.min.js", dest: "Scripts/Lib/table/jquery.floatThead.min.js"
                    },
                    {
                        src: "node_modules/tablesorter/dist/js/jquery.tablesorter.min.js", dest: "Scripts/Lib/table/jquery.tablesorter.min.js"
                    }
                ]
            },
            datatable: {
                files: [
                    { src: "node_modules/datatables.net/js/jquery.dataTables.min.js", dest: "Scripts/Lib/datatable/jquery.dataTables.min.js" },
                    { src: "node_modules/datatables.net-bs/js/dataTables.bootstrap.min.js", dest: "Scripts/Lib/datatable/dataTables.bootstrap.min.js" },
                    { src: "node_modules/datatables.net-fixedcolumns/js/dataTables.fixedColumns.min.js", dest: "Scripts/Lib/datatable/dataTables.fixedColumns.min.js" },
                    { src: "node_modules/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js", dest: "Scripts/Lib/datatable/dataTables.fixedHeader.min.js" }
                ]
            },
            datatablestyle: {
                files: [
                    {
                        src: "node_modules/datatables.net-bs/css/dataTables.bootstrap.min.css", dest: "Stylesheets/Lib/datatable/dataTables.bootstrap.min.css"
                    },
                    {
                        src: "node_modules/datatables.net-fixedcolumns-bs/css/fixedColumns.bootstrap.min.css", dest: "Stylesheets/Lib/datatable/fixedColumns.bootstrap.min.css"
                    },
                    {
                        src: "node_modules/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css", dest: "Stylesheets/Lib/datatable/fixedHeader.bootstrap.min.css"
                    }
                ]
            },
            fontawesome: {
                files: [
                    { src: "node_modules/font-awesome/css/font-awesome.min.css", dest: "Stylesheets/Lib/fontawesome/font-awesome.min.css" }
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