# API_HWP

       *Сериализация* представляет процесс преобразования какого-либо объекта в поток байтов. После преобразования мы можем этот поток байтов или записать на диск или сохранить его временно в памяти. А при необходимости можно выполнить обратный процесс - *Десериализацию*, то есть получить из потока байтов ранее сохраненный объект.
       
       
       
using System.Text.Json;
using System.Text.Json.Serialization;


    Как записать объекты .NET в формате JSON (сериализация):

jsonString = JsonSerializer.Serialize(weatherForecast);
File.WriteAllText(fileName, jsonString);


   Как считать JSON как объекты .NET (десериализация):
   
jsonString = File.ReadAllText(fileName);
weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString);
  
  
  
