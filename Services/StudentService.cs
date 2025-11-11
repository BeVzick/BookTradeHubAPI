using AutoMapper;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using BookTradeHubAPI.Repositories;

namespace BookTradeHubAPI.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepo, IBookService bookService, IMapper mapper)
    {
        _studentRepo = studentRepo;
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task CreateAsync(StudentCreateDto student) =>
        await _studentRepo.CreateAsync(_mapper.Map<Student>(student));

    public async Task<List<StudentGetDto>> GetAsync()
    {
        List<StudentGetDto> students = _mapper.Map<List<StudentGetDto>>(await _studentRepo.GetAllAsync());
        foreach (var student in students)
            student.Books = await _bookService.GetByOwnerAsync(student.Id);

        return students;
    }

    public async Task<StudentGetDto> GetAsync(string id)
    {
        Student? student = await _studentRepo.GetByIdAsync(id);
        if (student == null)
            throw new NullReferenceException($"Student with id:{id} doesn't exists");

        StudentGetDto getStudent = _mapper.Map<StudentGetDto>(student);
        getStudent.Books = await _bookService.GetByOwnerAsync(student.Id);

        return getStudent;
    }

    public async Task UpdateAsync(string id, StudentCreateDto student)
    {
        if (await _studentRepo.GetByIdAsync(id) == null)
            throw new NullReferenceException($"Student with id:{id} doesn't exists");

        Student newStudent = _mapper.Map<Student>(student);
        newStudent.Id = id;
        await _studentRepo.UpdateAsync(id, newStudent);
    }

    public async Task DeleteAsync(string id)
    {
        if (await _studentRepo.GetByIdAsync(id) == null)
            throw new NullReferenceException($"Student with id:{id} doesn't exists");

        await _studentRepo.DeleteAsync(id);
    }
}
