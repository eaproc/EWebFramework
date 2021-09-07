module.exports = function(grunt){

    // Configuration
    grunt.initConfig({
        uglify: {
            pages_scripts: {
                files:[ {
                    expand: true,     // Enable dynamic expansion.
                    //cwd: '/',      // Src matches are relative to this path.
                    src: ['pages/scripts/**/*.js'], // Actual pattern(s) to match.
                    ext: '.min.js',
                    extDot: 'first'
                }]
            },
            plugin_scripts: {
                files:[ {
                    expand: true,     // Enable dynamic expansion.
                    //cwd: '/',      // Src matches are relative to this path.
                    src: ['plugins/**/*.js'], // Actual pattern(s) to match.
                    ext: '.min.js',
                    extDot: 'first'
                }]
            }
        },
        cssmin: {
            target: {
                files: [{
                    expand: true,
                    //cwd: '/',
                    src: ['pages/css/**/*.css', '!pages/css/**/*.min.css'],
                    dest: '',
                    ext: '.min.css'
                }]
            }
        },
        clean: {
            options: {
                'no-write': false
            },
            pages_scripts: ['pages/scripts/**/*.min.js'],
            plugin_scripts: ['plugins/**/*.min.js']
        }

    });

    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-cssmin');


    grunt.registerTask('PageScripts', 'uglify:pages_scripts');
    grunt.registerTask('PluginScripts', 'uglify:plugin_scripts');
    grunt.registerTask('AllScripts', ['PageScripts','PluginScripts']);
    grunt.registerTask('CleanMinimizedJS', ['clean:pages_scripts','clean:plugin_scripts']);

    grunt.registerTask('AllCSS', ['cssmin:target']);

    grunt.registerTask('default', ['AllCSS','CleanMinimizedJS','AllScripts']);


};