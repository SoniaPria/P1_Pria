using System;

// JsonUtility .FromJson :
// Debe ser una clase/estructura simple
// marcada con el atributo Serializable

[Serializable]
public class QuestionCs
{
    public string category;
    public string type;
    public string difficulty;
    public string question;
    public string correct_answer;
    public string[] incorrect_answers;
}
