# RapidMVC
RapidMVC is light MVC/IoC framework written in C#. It's goal is to improve code quality, provide better encapsulation and decoupling, and in general provide a skeleton for building complex projects. It is heavily inspired by [StrangeIoC](https://github.com/strangeioc/strangeioc), but hopefully improves on certain aspects.

The framework is written with game developers in mind, specifically Unity3D, see [RapidMVC Unity3D integration](https://github.com/cpgames/RapidMVCUnity) (although can probably be used for other applications as well). It tries to accomplish the following:
* Fast integration and development (hence name "Rapid"). Given the limited time game programmers have to refactor and clean their code and the need to prototype and iterate fast, RapidMVC provides quick and easy solutions for both prototyping, and development.
* Easy to learn. If you are familiar with StrangeIoc, then this should be no-brainer, as it inherits many aspects from the former. However, in my opinion RapidMVC improves over StrangeIoC's steep learning curve by reducing amount of setup required, provides better starting platform (see [RapidMVC Unity3D integration](https://github.com/cpgames/RapidMVCUnity)), improves on error/exception output, and removes several unnecessary/rarely used (in my personal opinion) APIs which made StrangeIoC somewhat confusing to beginners.
* Light and thin. RapidMVC does not rely on any 3rd party code, it is under 600 lines of code, and it produces only 3 dlls which can be imported into your project.
* Last but not least, it's free and opensource. Code is well documented, and can be modified to your specific needs.

[RapidMVC Documentation](https://github.com/cpgames/RapidMVC/wiki)

[Doxygen Docs](https://cpgames.github.io/RapidMVC/html/index.html)
