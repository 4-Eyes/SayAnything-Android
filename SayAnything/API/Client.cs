using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SayAnything.Model;

namespace SayAnything.API
{
    public class Client
    {
        private const string Url = "http://localhost:8080/api/";

        private readonly HttpClient _client = new HttpClient();
        private readonly int _gameId;

        public Client(int gameId)
        {
            _gameId = gameId;
        }

        public async Task<bool> Join(User user)
        {
            var url = Url + "join";

            var t = new
            {
                userId = user.Id,
                gameId = _gameId
            };

            var content = new StringContent(JsonConvert.SerializeObject(t), Encoding.ASCII, "application/json");
            var response = await _client.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddQuestion(Question question)
        {
            var url = Url + "questions";

            question.GameId = _gameId;

            var content = new StringContent(JsonConvert.SerializeObject(question), Encoding.ASCII, "application/json");
            var response = await _client.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddAnswer(Answer answer)
        {
            var url = Url + "answers";

            var content = new StringContent(JsonConvert.SerializeObject(answer), Encoding.ASCII, "application/json");
            var response = await _client.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Question>> GetQuestions()
        {
            var url = Url + "questions/" + _gameId;
            var json = await _client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<List<Question>>(json);
        }

        public async Task<Question> GetQuestion(int id)
        {
            var url = Url + "question/" + _gameId + "/" + id;
            var json = await _client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<Question>(json);
        }

        public async Task<List<Answer>> GetAnswers(Question question)
        {
            var url = Url + "answers/" + _gameId + "/" + question.Id;
            var json = await _client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<List<Answer>>(json);
        }

        public async Task<List<string>> GetUsers()
        {
            var url = Url + "users";
            var response = await _client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<List<string>>(response);
        }
    }
}