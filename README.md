# RapidIoC
RapidIoC is a lightweight Inversion of Control/Dependency Injection framework written in C#. It helps improve code quality, simplify code maintenance and extendibility. Additionally, its provides a solid foundation for architecting complex projects. The framework is written with game developers in mind, specifically Unity3D, see [RapidIoC for Unity](https://github.com/cpgames/RapidIoCUnity) (although can easily be used for other applications as well).

While developing RapidIoC, I've tried to accomplish the following:
* **Fast integration and development** - given the limited time game programmers have to refactor and clean their code and the need to prototype and iterate fast, RapidIoC provides quick and easy solutions for both prototyping, and development.
* **Easy to learn**. If you are familiar with MVC (Model-View-Command) pattern and dependency injection, then picking up RapidIoC should be straight forward. Simply derive your class from View, register it with context, and any properties marked with *[Inject]* attribute will be automatically injected.  
* **Lean** - unlike similar MVC frameworks RapidIoC does away with a lot of code bloat (e.g. there are no Mediators) and you can use it to any degree you want, whether architecting everything around Context-View hierarchy, or only binding several shared instances.  
* **Powerful Signal API** - RapidIoC comes with an extensive Signal-Command API which greatly promotes better code decoupling.  
* **Free and open-source** - code is regularly updated, documented, and can be modified for your specific needs.
* **Threadsafe** - RapidIoC can be used in multithreaded code.

What is Inversion of Control and Dependency Injection, and how it can benefit you, read [here](https://www.tutorialsteacher.com/ioc/inversion-of-control).

RapidIoC is inspired by [StrangeIoC](http://strangeioc.github.io/strangeioc/exec.html) (they also have good explanation of IoC concepts).

[RapidIoC Documentation](https://github.com/cpgames/RapidIoC/wiki)
