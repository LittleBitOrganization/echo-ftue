# FTUE модуль

Модуль может быть использован для быстрой реализации туториалов внутри игровой нфраструктуры Little Bit
В него включены базовые инструменты для работы с камерой, выделением объектов, отправкой аналитики

## Зависимости

```json
"name": "com.littlebitgames.ftue",
"dependencies": {
    "com.littlebitgames.datastoragemodule": "~1.0.0",
    "com.littlebitgames.raycastsmodule": "~1.0.0",
    "com.littlebitgames.coremodule": "~1.0.0",
    "com.littlebitgames.savemodule": "~1.0.0",
    "com.littlebitgames.quests": "~0.4.5",
    "com.coffee.softmask-for-ugui": "~1.0.2",
    "com.dbrizov.naughtyattributes": "~2.1.4",
    "com.littlebitgames.warehouse" : "~1.1.3",
    "com.littlebitgames.descriptioncomponentsmodule": "~1.8.1",
    "com.littlebitgames.sequencelogic": "~1.1.0",
    "com.littlebitgames.uimodule": "~1.3.2",
    "com.littlebitgames.pool": "~1.0.4",
    "com.littlebitgames.cameramodule" : "~1.2.8",
    "com.littlebitgames.analytics" : "~2.1.5",
    "com.littlebitgames.environmentcore" : "~2.0.2"
  }
```

## Использование

Для корректной работы, помимо прочего, необходимо истанцировать следующие сущности:
  - ContainerHighlighterObjects
  - ScenarioLibrary
  - ScenarioFactory
  - HighlightView
  - HighlightPresenter
  - CameraPresenter
  - Bootstrap

Необходимо создать блоки сценариев, унаследовавшись от ScenarioBlockBase, далее проинициализировать 
Внутри блока можно создавать сценарии, описывающие последовательность действий

```C#

var scenario = NewScenario("FtueHireMower");
scenario.AddTrigger('...').ActionCloseWindows('...').ActionDialog('...').Backup();

AppendScenario(scenario);

```

Все сценарии вызываются последовательно, при необходимости создания неотложенного сценария см. класс <b>ForceInserterScenario</b>


Проинициализировав все сценарии, необходимо вручную запустить Bootstrap посредством вызова метода <b>Run()</b>

## Расширение возможностей системы;

Для добавления собственных методов напишите расширение для ComponentFactory

Пример реализованного расширения:


```C#

public static ComponentFactory WaitForFocusAppear(this ComponentFactory componentFactory, string key)
  {
    return componentFactory.Create<WaitForFocusAppear>(key);
  }

```



