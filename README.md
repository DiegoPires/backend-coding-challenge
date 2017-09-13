# Coveo Backend Coding Challenge
(inspired by https://github.com/busbud/coding-challenge-backend-c)

## Requirements

Design an API endpoint that provides auto-complete suggestions for large cities.

- The endpoint is exposed at `/suggestions`
- The partial (or complete) search term is passed as a querystring parameter `q`
- The caller's location can optionally be supplied via querystring parameters `latitude` and `longitude` to help improve relative scores
- The endpoint returns a JSON response with an array of scored suggested matches
    - The suggestions are sorted by descending score
    - Each suggestion has a score between 0 and 1 (inclusive) indicating confidence in the suggestion (1 is most confident)
    - Each suggestion has a name which can be used to disambiguate between similarly named locations
    - Each suggestion has a latitude and longitude

## "The rules"

- *You can use the language and technology of your choosing.* It's OK to try something new (tell us if you do), but feel free to use something you're comfortable with. We don't care if you use something we don't; the goal here is not to validate your knowledge of a particular technology.
- End result should be deployed on a public Cloud (Heroku, AWS etc. all have free tiers you can use).

## Advices

- **Try to design and implement your solution as you would do for real production code**. Show us how you create clean, maintainable code that does awesome stuff. Build something that we'd be happy to contribute to. This is not a programming contest where dirty hacks win the game.
- Feel free to add more features! Really, we're curious about what you can think of. We'd expect the same if you worked with us.
- Documentation and maintainability is a plus.
- Don't you forget those unit tests.
- We don’t want to know if you can do exactly as asked (or everybody would have the same result). We want to know what **you** bring to the table when working on a project, what is your secret sauce. More features? Best solution? Thinking outside the box?

## Sample responses

These responses are meant to provide guidance. The exact values can vary based on the data source and scoring algorithm

**Near match**

    GET /suggestions?q=Londo&latitude=43.70011&longitude=-79.4163

```json
{
  "suggestions": [
    {
      "name": "London, ON, Canada",
      "latitude": "42.98339",
      "longitude": "-81.23304",
      "score": 0.9
    },
    {
      "name": "London, OH, USA",
      "latitude": "39.88645",
      "longitude": "-83.44825",
      "score": 0.5
    },
    {
      "name": "London, KY, USA",
      "latitude": "37.12898",
      "longitude": "-84.08326",
      "score": 0.5
    },
    {
      "name": "Londontowne, MD, USA",
      "latitude": "38.93345",
      "longitude": "-76.54941",
      "score": 0.3
    }
  ]
}
```

**No match**

    GET /suggestions?q=SomeRandomCityInTheMiddleOfNowhere

```json
{
  "suggestions": []
}
```

## References

- Geonames provides city lists Canada and the USA http://download.geonames.org/export/dump/readme.txt

## Getting Started

Begin by forking this repo and cloning your fork. GitHub has apps for [Mac](http://mac.github.com/) and
[Windows](http://windows.github.com/) that make this easier.


## Implementation explanation

- Developed using Asp.net Core 2.0 with C# on a Mac. C# it's not the language I use on a day to day basis now, but it's a language I like and I will problably work with. It's my first time using .net Core, I thought it was a good opportunity to check what it has to offer, specially because I'm using a Mac to code.
- I choose to use a Mac because it's my main computer now, the one I use to code in other languages, and I heard good things about the support from .net core and from Visual Studio Code (the IDE I developed everything). I might say it was a little challenge to lost all the benefits from Visual Studio (I actually didnt't manage to debug yet, for example)
- Might say .net Core suffers kind of some problems of "edge" techs: hard to find people with the same problems, need to look a lot more on the incomplet docs, some mysterious errors and incompatibilitys to manage...
- The route of the API it's actually "api/v1/suggestions". It's a better practice to declare a version for future changes that don't break old stuff.
- Documentation was provided with Swagger. Just made a generic route to point everytime to it, if the person is not on the API.
- Tried to comment code that it's more "Magic", otherwise, most of the code speak for himself.
- The system it's quite simple, but I chose to implement a Service Pattern just to abstract all the logic from the controller and let it clean. IoC comes out of the box now, so it`s there, and it actually helps the tests.
- Tried to implement a test to the controller with a mock of the service, just missing some little pieces to make it work 100%. The library still don't reconize the mock to execute.
- Implemented a little test to my service layer too while developpement the layer. It runs direct to Azure Search.
- Azure Search was chosen because I was already thinking to host the API on Azure and found this IaaS that looked promising. It took all the problems of hosting a database with support for ElasticSearch and it has a strong case about been easy to scale. Founding a start tutorial was not so complicated, just for some details of implementation it's harder to find material. The final implementation it's missing search for the geolocalization and the score going from 0 to 1 (Right now it has it's own algorithm)
- Overall, I would say it's a good first MVP, need some more time for the details, but offers enought room to grow. And definitly, all the tech stack played a little role on the adaptation.