# Zoo Management Module — Отчёт по реализации

## A. Реализованный функционал

В проекте реализованы все требуемые сценарии (use cases):

1. Добавление и удаление животных — реализовано в `AnimalsController`, с использованием модели `Animal` и интерфейса `IAnimalRepository`.

2. Добавление и удаление вольеров — реализовано в `EnclosuresController`, с использованием модели `Enclosure` и интерфейса `IEnclosureRepository`.

3. Перемещение животных между вольерами — реализовано в сервисе `AnimalTransferService`, логика перемещения инкапсулирована в сущностях `Animal` и `Enclosure`.

4. Просмотр расписания кормлений — реализовано в `FeedingController` и `FeedingOrganizationService`.

5. Добавление кормлений — реализовано в `FeedingController`, добавление обрабатывается в `FeedingOrganizationService`.

6. Отметка выполненного кормления — реализовано через метод `MarkFedAsync` в `FeedingOrganizationService`.

7. Получение статистики по зоопарку — реализовано в `StatisticsController` и сервисе `ZooStatisticsService`.

---

## B. Применённые архитектурные подходы

### Структура проекта

Проект построен по принципам Clean Architecture:

- Слой `Domain` содержит сущности и бизнес-логику: `Animal`, `Enclosure`, `FeedingSchedule`, а также доменные события `AnimalMovedEvent` и `FeedingTimeEvent`.

- Слой `Application` содержит сервисы и интерфейсы: `AnimalTransferService`, `FeedingOrganizationService`, `ZooStatisticsService`, а также репозитории и диспетчер событий.

- Слой `Infrastructure` реализует InMemory-хранилища и взаимодействие с событиями (например, `ConsoleEventDispatcher`).

- Слой `Presentation` реализует REST API контроллеры: `AnimalsController`, `EnclosuresController`, `FeedingController`, `StatisticsController`.

### Концепции Domain-Driven Design

В проекте применены ключевые принципы DDD:

- Сущности инкапсулируют поведение: методы `Animal.Treat()`, `Animal.MoveToEnclosure()`, `Enclosure.AddAnimal()` содержат правила предметной области.

- Используются Value Objects на основе enum’ов: `AnimalGender`, `EnclosureType`, `HealthStatus`.

- Реализованы доменные события, публикуемые через `IEventDispatcher`.

- Все зависимости между слоями идут внутрь, `Domain` не зависит от `Application` и других уровней.

- Репозитории описаны через интерфейсы и реализованы отдельно в `Infrastructure`.

---

## C. Покрытие тестами

Для модуля реализованы unit-тесты с использованием `xUnit`, `Moq` и `coverlet`. Тестируются:

- Бизнес-логика сущностей: `Animal`, `Enclosure`

- Сервисы прикладного уровня: `AnimalTransferService`, `ZooStatisticsService`
