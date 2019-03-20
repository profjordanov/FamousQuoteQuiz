(function () {
  $(document).ready(function() {

    $("#set-binary-choice-btn").click(function () {
      showBinaryChoiceQuizPage();

      $.fancybox.close();
      
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
  });
})(baseSurviceQuizUrl);

(function () {
  $(document).ready(function() {

    $("#set-multiple-choice-btn").click(function () {
      showMultipleChoiceQuizPage();

      $.fancybox.close();

      const querydata = {
        initialId: 0
      };

      const url = baseSurviceQuizUrl + "multiple-choice-question" + encodeQueryData(querydata);

      $.ajax({
        url: url,
        type: 'GET',
        responseType: 'application/json',
        success: function(data) {
          applyMultipleChoiceQuestion(data);
        },
        error: function() {
          $.fancybox("#error");
        }
      });
    });
  });
})(baseSurviceQuizUrl);

function showBinaryChoiceQuizPage () {
  $("#binary-choice-question-quiz-page").show();
  $("#multiple-choice-question-quiz-page").hide();
}

function showMultipleChoiceQuizPage () {
  $("#binary-choice-question-quiz-page").hide();
  $("#multiple-choice-question-quiz-page").show();
}