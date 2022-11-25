var helpers;
(function (helpers) {
    // https://www.youtube.com/watch?v=I_zFlBKgl5s
    // Simple Logger class that exposes a log method
    class Logger {
        log(text) {
            console.log(text);
        }
    }
    // Method to return an instance of the Logger class
    function getLogger() {
        return new Logger();
    }
    helpers.getLogger = getLogger;
})(helpers || (helpers = {}));
//# sourceMappingURL=helpers.js.map