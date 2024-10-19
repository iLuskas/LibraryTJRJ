using Asp.Versioning;
using LibraryTJRJ.Application.Subjects.Commands.CreateSubject;
using LibraryTJRJ.Application.Subjects.Commands.DeleteSubject;
using LibraryTJRJ.Application.Subjects.Commands.UpdateSubject;
using LibraryTJRJ.Application.Subjects.Queries.GetSubject;
using LibraryTJRJ.Application.Subjects.Queries.ListSubjects;
using LibraryTJRJ.Application.Subjects.Queries.ListSubjectsByBook;
using LibraryTJRJ.Contracts.Subjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTJRJ.Api.Controllers
{
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/suject")]
    public class SubjectController(ISender sender) : ApiController
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectRequest request)
        {
            var createSubjectResult = await _sender.Send(
                new CreateSubjectCommand(
                    request.Description
                )
            );

            return createSubjectResult.Match(
                subject => CreatedAtAction(
                    nameof(GetSubject),
                    new { SubjectId = subject.Id },
                    new SubjectResponse(subject.Id, subject.Description)),
                Problem);
        }

        [HttpPut("{subjectId:guid}")]
        public async Task<IActionResult> Update(Guid subjectId, [FromBody] SubjectRequest request)
        {
            var command = new UpdateSubjectCommand(
                        SubjectId: subjectId,
                        request.Description
            );

            var result = await _sender.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{subjectId:guid}")]
        public async Task<IActionResult> Delete(Guid subjectId)
        {
            var command = new DeleteSubjectCommand(subjectId);

            var result = await _sender.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> ListSubjects()
        {
            var listSubjectsResult = await _sender.Send(new ListSubjectsQuery());

            return listSubjectsResult.Match(
                subjects => Ok(subjects.ConvertAll(subject => new SubjectResponse(subject.Id, subject.Description))),
                Problem);
        }

        [HttpGet("{subjectId:guid}")]
        public async Task<IActionResult> GetSubject(Guid subjectId)
        {
            var command = new GetSubjectQuery(subjectId);

            var getSubjectResult = await _sender.Send(command);

            return getSubjectResult.Match(
                subject => Ok(new SubjectResponse(subject.Id, subject.Description)),
                Problem);
        }

        [HttpGet("{bookId}/subjects")]
        public async Task<IActionResult> GetSubjectsByBook(Guid bookId)
        {
            var subjectsResult = await _sender.Send(new ListSubjectsByBookQuery(bookId));

            return subjectsResult.Match(
                subjects => Ok(subjects.ConvertAll(subject =>
                    new SubjectResponse(subject.Id, subject.Description))
                ),
                Problem);
        }
    }
}
