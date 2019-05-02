# RapidIoC
RapidIoC is light IoC (Inversion of Control)/DI (Dependency Injection) framework written in C#. It's goal is to improve code quality by promoting better encapsulation, loose coupling, and high cohesion. A goal of RapidIoC is to provide a solid foundation for architecting more complex projects. The framework is written with game developers in mind, specifically Unity3D, see [RapidIoC Unity3D integration](https://github.com/cpgames/RapidIoCUnity) (although can easily be used for other applications as well).

[What is Inversion of Control](https://www.tutorialsteacher.com/ioc/inversion-of-control)

 Developing RapidIoC, we've tried to accomplish the following:
* Fast integration and development (hence name "Rapid"). Given the limited time game programmers have to refactor and clean their code and the need to prototype and iterate fast, RapidIoC provides quick and easy solutions for both prototyping, and development.
* Easy to learn. If you are familiar with StrangeIoc, then this should be no-brainer, as it inherits many aspects from the former. However, in my opinion RapidIoC improves over StrangeIoC's steep learning curve by reducing amount of setup required, provides better starting platform (see [RapidIoC Unity3D integration](https://github.com/cpgames/RapidIoCUnity)), improves on error/exception output, and removes several unnecessary/rarely used (in my personal opinion) APIs which made StrangeIoC somewhat confusing to beginners.
* Light and thin. RapidIoC does not rely on any 3rd party code, it is under 600 lines of code, and it produces only 3 dlls which can be imported into your project.
* Last but not least, it's free and opensource. Code is well documented, and can be modified to your specific needs.

RapidIoC is inspired by [StrangeIoC](https://github.com/strangeioc/strangeioc), but hopefully improves on certain aspects.

[RapidIoC Documentation](https://github.com/cpgames/RapidIoC/wiki)

[Doxygen Docs](https://cpgames.github.io/RapidIoC_dox/html/index.html)
