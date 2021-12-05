Сервис по пополнению баланса сотовой связи. 
---

Необходимо разработать web api
На вход поступает номер телефона и сумма

Исходя из номера телефона необходимо определить оператора и вызвать сервис нужного оператора
Вызов сервиса каждого оператора реализовать в виде заглушки, которая всегда возвращает успешный ответ
Для каждого оператора своя заглушка
Определение оператора:
Префикс 701 - актив
префиксы 777, 705 - билайн
префиксы 707, 747 - теле2
префиксы 700, 708 - алтел

Понятием возможности портирования номера через MNP - пренебречь

Необходимо покрыть сервис unit тестами(любая библиотека на выбор)
Также необходимо в сервисе предусмотреть ошибки для вызывающих систем(структура на усмотрение)
Предусмотреть локализацию на 2-х языках ошибок(казахский, русский) - реализация локализации на усмотрение разработчика
Сделать логирование сервиса, логирование на усмотрение разработчика
Сделать сохранение платежей в БД, структура БД на усмотрение разработчика

Пример запроса:
---
{

  "phone": "7010100111",
  "amount": 500,
  "externalNumber": "f0c4g8fj-l06a-01f3-k18d-48d75c2fh730"
  
}

Заголовки:

RequestId: fkc4a3fd-e23b-41f5-a18d-48d75c2fe735

Здесь на выбор две культуры, необходимо использовать одну из них. Если не передать данный заголовок, то будет использована ru культура.

Content-Language: kk/ru

Пример ответа:
---

{

    "statusCode": 700,
    "message": "Платеж не удался. Повторяющийся ExternalNumber"
    
}

Коды ошибок:
---

Success : 200

ServiceUnavailable : 300

UnableError : 400

ProviderNotFound : 500

ValidationProblem : 600

DuplicateExternalNumber : 700


