# NetCore-Web-Api-Example

Примеры запросов для проверки:
/api/users
/api/users/1
/api/users/1/albums
/api/albums
/api/albums/1

Для проверки по дополнительным требованиям:

1) Формат ответа JSON или XML можно выбрать, указав в запросе к api соответствующее значение в Accept header: application/json или application/xml.

2) Для того, чтобы в ответе приходил Email пользователя нужно в заголовке From указать email этого пользователя, например, Sincere@april.biz. Все остальные email отображаться не будут.