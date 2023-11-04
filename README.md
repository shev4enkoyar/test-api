# Loginet test API

Сервис написан на .NET 6 ASP.NET Core WebAPI

Приложение готово к запуску в docker-compose.
Для запуска без использования docker требуется указать в appsettings.json настройки подключения к базе данных.

Решение построенно согласно clean code.

## Принцип работы

1. Сервис получает запрос на метод контроллера
2. Запускается pipline
3. В DatabaseContentComplementBehaviour производится проверка на наличие данных в базе данных
    - Если данных нет, то запрашиваются с внешнего API, в противном случае pipline проходит дальше
4. Выполняется запрос к базе данных на получение данных
    - Если в базе данных объект не найден, производится запрос к внешнему API
5. Полученные данные возвращаются на метод контроллера
6. Метод контроллера возвращает данные клиенту

## Переменные среды

> Переменные среды docker контейнера являются приоритетными

При использовании docker-compose можно указать параметры среды для изменения поведения:

- ApplicationSettings__DatabaseAddress (имя контейнера базы данных или адресс)
- ApplicationSettings__DatabaseName (имя базы данных)
- ApplicationSettings__DatabaseUser (пользователь базы данных)
- ApplicationSettings__DatabasePassword (пароль пользователя базы данных)
- ApplicationSettings__DatabasePort (порт базы данных)
- ApplicationSettings__JsonPlaceholderApiUrl (базовый адресс для обращения к внешнему API)
- ApplicationSettings__JsonPlaceholderApiDelayInSec (задержка повторного запроса к внешнему API при статусах отличных от
  2xx)
- ApplicationSettings__JsonPlaceholderApiNumRetry (колличество повторных запросов к внешнему API при статусах отличных
  от 2xx)
- ApplicationSettings__DefaultCacheDuration (время действия кеша при обращении к API)

Аналогичный результат будет и при изменении appsettings.json

## Возможные запросы к API

На API включен Swagger (http://localhost/swagger/index.html)

- Получение списка всех пользователей (http://localhost/api/users)
- Получение пользователя по id (http://localhost/api/users/1)
- Получение списка всех альбомов (http://localhost/api/albums)
- Получение всех альбомов одного пользователя (http://localhost/api/albums?userId=1)
- Получение альбома по id (http://localhost/api/albums/1)

## Использованные библиотеки

- MeditR
- AutoMapper
- EntityFrameworkCore
- Npgsql.EntityFrameworkCore.PostgreSQL
- Polly