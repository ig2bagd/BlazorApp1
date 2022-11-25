//export function incrementCounter(currentCount) {
//   return ++currentCount;
//}

class Counter {
   constructor() {
      this.currentCount = 0;
   }

   incrementCounter() {
      return ++this.currentCount;
   }
}

const counter = new Counter();
export { counter }