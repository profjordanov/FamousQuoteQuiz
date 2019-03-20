(function () {
  $(document).ready(function() {
    const querydata = {
      initialId: 0
    };
    const url = baseSurviceQuizUrl + "binary-choice-question" + encodeQueryData(querydata);
    $.ajax({
      url: url,
      type: 'GET',
      responseType: 'application/json',
      success: function(data) {
        applyBinaryChoiceQuestion(data);
      },
      error: function() {
        $.fancybox("#error");
      }
    });

  });
})(baseSurviceQuizUrl);

(function() {
  $(document).ready(function() {
    $("#true-bin-answ-btn").click(function() {
      const currentQuestionId = $("#bin-quote-id").text();
      const questIsTrue = $("#bin-quote-isTrue").text();
      const correctAuthor = $("#bin-quote-correct-author").text();
      const lastBinaryChoiceQuestionId = localStorage.lastBinaryChoiceQuestionId;

      if (questIsTrue == "true") {
        if (currentQuestionId == lastBinaryChoiceQuestionId) {
          showFinalCorrectUserAnswer(correctAuthor);
        } else {
          showCorrectUserAnswer(correctAuthor);
          showNextBtn();
        }
      } else {
        if (currentQuestionId == lastBinaryChoiceQuestionId) {
          showFinalIncorrectUserAnswer(correctAuthor);
        } else {
          showIncorrectUserAnswer(correctAuthor);
          showNextBtn();
        }
      }
    });

    $("#false-bin-answ-btn").click(function() {
      const currentQuestionId = $("#bin-quote-id").text();
      const questIsTrue = $("#bin-quote-isTrue").text();
      const correctAuthor = $("#bin-quote-correct-author").text();
      const lastBinaryChoiceQuestionId = localStorage.lastBinaryChoiceQuestionId;
      if (questIsTrue == "true") {
        if (currentQuestionId == lastBinaryChoiceQuestionId) {
          showFinalIncorrectUserAnswer(correctAuthor);
        } else {
          showIncorrectUserAnswer(correctAuthor);
          showNextBtn();
        }
      } else {
        if (currentQuestionId == lastBinaryChoiceQuestionId) {
          showFinalCorrectUserAnswer(correctAuthor);
        } else {
          showCorrectUserAnswer(correctAuthor);
          showNextBtn();
        }
      }
    });
  });
})();

(function() {
  $(document).ready(function() {

    $("#next-bin-quest-btn").click(function() {
      const currentQuoteId = $("#bin-quote-id").text();

      const querydata = {
        initialId: currentQuoteId
      };

      const url = baseSurviceQuizUrl + "binary-choice-question" + encodeQueryData(querydata);

      $.ajax({
        url: url,
        type: 'GET',
        responseType: 'application/json',
        success: function(data) {
          applyBinaryChoiceQuestion(data);
          showYesNoBtns();
        },
        error: function() {
          $.fancybox("#error");
        }
      });
    });
  });
})();

function applyBinaryChoiceQuestion(data) {
  $("#bin-quote-id").text(data.id);
  $("#bin-quote-isTrue").text(data.isTrue);
  $("#bin-quote-content").text(data.quote);
  $("#bin-quote-author").text(data.questionableAuthor);
  $("#bin-quote-correct-author").text(data.correctAuthor);
}

function showNextBtn() {
  $("#who-is").hide();
  const correctAuthor = $("#bin-quote-correct-author").text();
  $("#bin-quote-correct-answer").show();
  $("#bin-quote-correct-answer").text("by " + correctAuthor);

  $("#next-bin-quest-btn").show();
  $("#yes-no-buttons").hide();
}

function showYesNoBtns() {
  $("#bin-quote-correct-answer").hide();
  $("#who-is").show();

  $("#yes-no-buttons").show();
  $("#next-bin-quest-btn").hide();
}