using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebSocket.SignalR.Models.DTOs;

namespace WebSocket.SignalR.Extensions
{
    public static class ResultExtensions
    {
        public static ResultDTO<T> FromResult<T>(this Result<T> result) =>
            ResultDTO<T>.Create(result.IsSuccess, result.ValueOrDefault, result.Errors.Select(err => err.Message).ToList(), result.Successes.Select(err => err.Message).ToList());
        public static ResultDTO FromResult(this Result result) =>
            ResultDTO.Create(result.IsSuccess, result.Errors.Select(err => err.Message).ToList(), result.Successes.Select(err => err.Message).ToList());

        public static ResultDTO FromIdentityResult(this IdentityResult result) =>
            ResultDTO.Create(result.Succeeded, result.Errors.Select(err => err.Description).ToList(), []);
    }
}
