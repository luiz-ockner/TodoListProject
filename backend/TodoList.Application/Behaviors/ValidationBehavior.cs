using FluentValidation;
using MediatR;

namespace TodoList.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> // Aplica-se a todos os Requests (Commands ou Queries)
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public  async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // Executa todos os validadores para o TRequest
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // Coleta todos os erros de validação
                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Any())
                {
                    // Se houver erros, lança uma exceção.
                    // O tratamento dessa exceção será feito globalmente na API (próximo passo).
                    throw new ValidationException(failures);
                }
            }

            // Se passar na validação, segue para o próximo behavior ou para o Handler (next)
            return await next();
        }
    }
}
