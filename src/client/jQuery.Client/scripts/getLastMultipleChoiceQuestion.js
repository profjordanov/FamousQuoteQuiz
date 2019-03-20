(function() {
  $(document).ready(function() {

    $.ajax({
      url: baseSurviceQuizUrl + "last-multiple-choice-question",
      type: 'GET',
      responseType: 'application/json',
      success: function(data) {
        storeLastMultipleChoiceQuestionId(data);
      },
      error: function() {
        $.fancybox("#error");
      }
    });

  });
})(baseSurviceQuizUrl);

function storeLastMultipleChoiceQuestionId(response) {
  localStorage.setItem('lastMultipleChoiceQuestionId', response.id);
}