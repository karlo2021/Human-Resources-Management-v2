import { Meeting } from "../meetings/meeting";

export class Person {
  constructor(
    public id?: number,
    public name?: string,
    public category?: string,
    public birth?: string,
    public rating?: number,
    public description?: string,
    public meetings?: Meeting[]
  ) {  }
}
