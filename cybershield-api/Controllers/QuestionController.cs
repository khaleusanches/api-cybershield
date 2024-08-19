﻿using cybershield_api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;

namespace cybershield_api.Controllers
{
    [ApiController]
    [Route("Question")]
    public class QuestionController : Controller
    {
        StreamReader questions_file = new StreamReader("Models/QuestionJ.Json");
        [HttpGet]
        public async Task<IActionResult> getRandomQuestion()
        {
            Random randomizer = new Random();
            var questions_json = await questions_file.ReadToEndAsync();
            var questions = JsonConvert.DeserializeObject<List<GetQuestionModel>>(questions_json);
            var question_num = randomizer.Next(questions.Count());
            var question = questions[question_num];

            return Ok(question);
        }
        [HttpGet("{id}/{answer}")]
        public async Task<IActionResult> getCorrectAnswer(int id, int answer)
        {
            var questions_json = await questions_file.ReadToEndAsync();
            var questions = JsonConvert.DeserializeObject<List<QuestionModel>>(questions_json);
            for (int i = 0; i < questions.Count(); i++)
            {
                if (questions[i].id_question == id)
                {
                    if (questions[i].Correct_answer == answer)
                    {
                        return Ok("resposta correta");
                    }
                    else
                    {
                        return Ok("resposta errada");
                    }
                }
            }
            return Ok();
        }
    }
}