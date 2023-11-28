


namespace csharp_gregslist.Services;

public class CarsService
{

  private readonly CarsRepository _repository;

  public CarsService(CarsRepository repository)
  {
    _repository = repository;
  }

  internal List<Car> GetCars()
  {
    List<Car> cars = _repository.GetCars();
    return cars;
  }
  internal Car GetCarById(int carId)
  {
    Car car = _repository.GetCarById(carId);

    if (car == null)
    {
      throw new Exception($"Invalid id: {carId}");
    }

    return car;
  }

  internal Car CreateCar(Car carData)
  {
    Car car = _repository.CreateCar(carData);
    return car;
  }

  internal string DestroyCar(int carId)
  {
    Car car = GetCarById(carId);

    _repository.DestroyCar(carId);

    return $"{car.Make} {car.Model} has been destroyed!";
  }



  internal Car UpdateCar(int carId, Car carData)
  {
    Car originalCar = GetCarById(carId);

    // originalCar.Make = carData.Make; NOTE can't change make if we don't allow them to here
    // NOTE ?? is null coalescence operator, if carData.Model is null, it assigns the model to what it originally was
    originalCar.Model = carData.Model ?? originalCar.Model;
    originalCar.Year = carData.Year ?? originalCar.Year;
    originalCar.Price = carData.Price ?? originalCar.Price;
    originalCar.Mileage = carData.Mileage ?? originalCar.Mileage;
    originalCar.Runs = carData.Runs ?? originalCar.Runs;

    _repository.UpdateCar(originalCar);

    return originalCar;
  }
}
