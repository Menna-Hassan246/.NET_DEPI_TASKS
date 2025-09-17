 const advices = [
      "Believe in yourself",
      "Stay positive ",
      "Never stop learning ",
      "Be kind to others ",
      "Take breaks and rest ",
      "Small steps lead to big changes",
      "Keep going, you’re doing great"
    ];

    function getAdvice(){
      const random = advices[Math.floor(Math.random() * advices.length)];
      document.getElementById("advice").textContent = random;
    }