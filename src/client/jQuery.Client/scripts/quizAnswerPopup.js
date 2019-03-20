function showCorrectUserAnswer (correctAuthor) {
  $("#correct-author-answer").text(correctAuthor);
  $.fancybox("#correct-user-answer");
}

function showIncorrectUserAnswer (correctAuthor) {
  $("#incorrect-author-answer").text(correctAuthor);
  $.fancybox("#incorrect-user-answer");
}

function showFinalCorrectUserAnswer (correctAuthor) {
  $("#correct-author-final-answer").text(correctAuthor);
  $.fancybox("#correct-user-answer-quiz-end-popup");
}

function showFinalIncorrectUserAnswer (correctAuthor) {
  $("#incorrect-author-final-answer").text(correctAuthor);
  $.fancybox("#incorrect-user-answer-quiz-end-popup");
}