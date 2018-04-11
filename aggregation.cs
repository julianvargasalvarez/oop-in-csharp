class Vehicle {
  private Person _driver;
  private Engine _engine;

  public Driver {get{ return _driver;}}

  public Vehicle(Person driver){
    _driver = driver;
    _engine = new Engine();
  }

  public Move() {
    _engine.Start();
    System.Console.WriteLine(_driver.Name + " is driving the vehicle");
  }

}

class Person {
  public string Name {get; set;}
}

class Engine {
  public void Start(){
  }
}

void main(void) {
  Person driver = new Person(){ Name="Pedro Paramo"}
  Vehicle myCar = new Vehicle(driver);
  myCar.Move();
  delete myCar;
  System.Console.WriteLine(driver);
}
