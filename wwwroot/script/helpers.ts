namespace helpers {
   // https://www.youtube.com/watch?v=I_zFlBKgl5s
   // Simple Logger class that exposes a log method
   class Logger {
      public log(text: string) {
         console.log(text);
      }
   }

   // Method to return an instance of the Logger class
   export function getLogger(): Logger {
      return new Logger();
   }
}
