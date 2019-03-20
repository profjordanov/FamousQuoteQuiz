(function() {
  $(document).ready(function() {

    $.ajax({
      url: baseSurviceQuizUrl + "last-binary-choice-question",
      type: 'GET',
      responseType: 'application/json',
      success: function(data) {
        storeLastBinaryChoiceQuestionId(data);
      },
      error: function() {
        $.fancybox("#error");
      }
    });

  });
})(baseSurviceQuizUrl);

function storeLastBinaryChoiceQuestionId(response) {
  localStorage.setItem('lastBinaryChoiceQuestionId', response.id);
}