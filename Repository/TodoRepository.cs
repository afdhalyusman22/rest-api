using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Todo;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repository
{
    public interface ITodoRepository
    {
        Task<CommonResponse> GetAll();
        Task<CommonResponse> GetById(int id);
        Task<CommonResponse> GetIncomingTodo(RequestIncomingTodo request);
        Task<CommonResponse> AddTodo(RequestAddTodo request);
        Task<CommonResponse> UpdateTodo(RequestUpdateToDo request);
        Task<CommonResponse> DeleteTodo(int id);
        Task<CommonResponse> UpdateComplete(RequestCompleteToDo request);
        Task<CommonResponse> UpdateDoneTodo(RequestDoneToDo request);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly ConnectionString _connectionString;
        private readonly CommonResponse _response;

        public TodoRepository(ConnectionString connectionString, CommonResponse response)
        {
            _connectionString = connectionString;
            _response = response;
        }

        public async Task<CommonResponse> GetAll()
        {
            try
            {
                const string query = @"select Id, Title, Description,Complete, ExpiredDate from todo";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.QueryAsync<ToDo>(query);

                    _response.status = 1;
                    _response.Message = "Success";
                    _response.Data = result;
                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<CommonResponse> GetById(int id)
        {
            try
            {
                const string query = @"select Id, Title, Description,Complete, ExpiredDate from todo where Id = @Id";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.QueryAsync<ToDo>(query, new { Id = id });

                    _response.status = 1;
                    _response.Message = "Success";
                    _response.Data = result;
                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }
            return _response;
        }

        public async Task<CommonResponse> GetIncomingTodo(RequestIncomingTodo request)
        {
            try
            {
                const string query = @"select Id, Title, Description,Complete, ExpiredDate from todo
                                        where ExpiredDate >= @startdate and ExpiredDate <= @enddate";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.QueryAsync<ToDo>(query, new { startdate = request.startdate + " 00:00:00", enddate = request.enddate + " 23:59:00"});

                    _response.status = 1;
                    _response.Message = "Success";
                    _response.Data = result;
                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CommonResponse> AddTodo(RequestAddTodo request)
        {
            try
            {
                const string query = @"INSERT INTO [dbo].[Todo]
           ([Title], [Description], [ExpiredDate], [Complete])
            VALUES ( @Title, @Description, @ExpiredDate, @Complete)";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.ExecuteAsync(query,
                        new { Title = request.Title, Description = request.Description, ExpiredDate = DateTime.Now.AddDays(7), Complete = 0 });

                    if (result > 0)
                    {
                        _response.status = 1;
                        _response.Message = "Success Add Todo";
                    }

                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CommonResponse> UpdateTodo(RequestUpdateToDo request)
        {
            try
            {
                const string query = @"UPDATE [dbo].[Todo]
                                     SET
                                    [Title] = @Title,
                                    [Description] = @Description,
                                    [Complete] = @Complete
                                     WHERE [Id] =  @Id ";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.ExecuteAsync(query,
                        new { Title = request.Title, Description = request.Description, Complete = request.Complete, Id = request.Id });

                    if (result > 0)
                    {
                        _response.status = 1;
                        _response.Message = "Success Update Todo";
                    }

                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CommonResponse> DeleteTodo(int id)
        {
            try
            {
                const string query = @"DELETE FROM [dbo].[Todo]
                                     WHERE [Id] =  @Id ";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.ExecuteAsync(query,
                        new { Id = id });

                    if (result > 0)
                    {
                        _response.status = 1;
                        _response.Message = "Success Delete Todo";
                    }
                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CommonResponse> UpdateComplete(RequestCompleteToDo request)
        {
            try
            {
                const string query = @"UPDATE [dbo].[Todo]
                                     SET
                                    [Complete] = @Complete
                                     WHERE [Id] =  @Id ";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.ExecuteAsync(query,
                        new { Complete = request.Complete, Id = request.Id });

                    if (result > 0)
                    {
                        _response.status = 1;
                        _response.Message = "Success Update Complete Todo";
                    }

                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }

            return _response;
        }

        public async Task<CommonResponse> UpdateDoneTodo(RequestDoneToDo request)
        {
            try
            {
                const string query = @"UPDATE [dbo].[Todo]
                                     SET
                                    [isDone] = @isDone
                                     WHERE [Id] =  @Id ";

                using (var conn = new SqlConnection(_connectionString.Value))
                {
                    var result = await conn.ExecuteAsync(query,
                        new { isDone = request.isDone , Id = request.Id });

                    if (result > 0)
                    {
                        _response.status = 1;
                        _response.Message = "Success Update Done Todo";
                    }
                }
            }
            catch (Exception ex)
            {
                _response.status = 0;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
