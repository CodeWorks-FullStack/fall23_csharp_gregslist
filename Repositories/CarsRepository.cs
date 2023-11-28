




namespace csharp_gregslist.Repositories;

public class CarsRepository
{
  // NOTE creates a connection to our database using our connection string in appsettings.development.json
  private readonly IDbConnection _db;

  public CarsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Car> GetCars()
  {
    string sql = "SELECT * FROM cars;";

    // _db talks to our database, and the method called from it is a dapper method

    // Query takes in a type to return, and it accepts an argument which is our string of sql

    // Query returns an IEnumerable<Car>, but we want a List<Car> so we call the .ToList() method
    List<Car> cars = _db.Query<Car>(sql).ToList();
    return cars;
  }

  internal Car GetCarById(int carId)
  {
    // string sql = $"SELECT * FROM cars WHERE id = {carId};"; NEVER INTERPOLATE INSIDE OF SQL STATEMENTS WITH DATA FROM USER

    string sql = "SELECT * FROM cars WHERE id = @carId;"; // Dapper will look through the second argument passed to it for a property on an object called "carId" and injects the value for us with added sanitization


    // We call the .FirstOrDefault method which says return the first row from this sql statement, or default to return null
    //                            new {carId: 1}
    Car car = _db.Query<Car>(sql, new { carId }).FirstOrDefault();
    return car;
  }

  internal Car CreateCar(Car carData)
  {
    // NOTE two seperate sql statements, Insert would only return how many rows were affected
    string sql = @"
    INSERT INTO cars (make, model, year, price, imgUrl, runs, mileage)
    VALUES (@Make, @Model, @Year, @Price, @ImgUrl, @Runs, @Mileage);
    
    SELECT * FROM cars WHERE id = LAST_INSERT_ID();"; // SELECT_LAST_INSERT_ID() is a sql function that gets the id of the last inserted row in our table

    Car car = _db.Query<Car>(sql, carData).FirstOrDefault();
    return car;
  }

  internal void DestroyCar(int carId)
  {
    string sql = "DELETE FROM cars WHERE id = @carId LIMIT 1;";

    _db.Execute(sql, new { carId });
  }


  internal void UpdateCar(Car originalCar)
  {
    string sql = @"
    UPDATE cars
    SET
    model = @Model,
    price = @Price,
    year = @Year,
    mileage = @Mileage,
    runs = @Runs
    WHERE id = @Id
    ;";

    _db.Execute(sql, originalCar);

  }
}
