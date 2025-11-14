using AutoMapper;
using BookTradeHubAPI.Models;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using BookTradeHubAPI.Repositories;

namespace BookTradeHubAPI.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IBookRepository _bookRepo;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public StudentService(IStudentRepository studentRepo, IBookRepository bookRepo, JwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher, IMapper mapper)
    {
        _studentRepo = studentRepo;
        _bookRepo = bookRepo;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task CreateAsync(StudentCreateDto student)
    {
        if (await _studentRepo.GetByEmailAsync(student.Email) != null)
            throw new InvalidOperationException($"Student width email:{student.Email} exists");

        Student studentCreate = _mapper.Map<Student>(student);
        studentCreate.Password = _passwordHasher.HashPassword(studentCreate.Password);
        await _studentRepo.CreateAsync(studentCreate);
    }

    public async Task<List<StudentGetDto>> GetAsync()
    {
        List<StudentGetDto> students = _mapper.Map<List<StudentGetDto>>(await _studentRepo.GetAllAsync());
        foreach (var student in students)
            student.Books = _mapper.Map<List<BookGetDto>>(await _bookRepo.GetByOwnerIdAsync(student.Id));

        return students;
    }

    public async Task<StudentGetDto> GetAsync(string id)
    {
        Student? student = await _studentRepo.GetByIdAsync(id);
        if (student == null)
            throw new NullReferenceException($"Student with id:{id} doesn't exists");

        StudentGetDto getStudent = _mapper.Map<StudentGetDto>(student);
        getStudent.Books = _mapper.Map<List<BookGetDto>>(await _bookRepo.GetByOwnerIdAsync(student.Id));

        return getStudent;
    }

    public async Task<StudentGetDto> GetByEmailAsync(string email)
    {
        Student? student = await _studentRepo.GetByEmailAsync(email);
        if (student == null)
            throw new NullReferenceException($"Student with email:{email} doesn't exists");

        StudentGetDto getStudent = _mapper.Map<StudentGetDto>(student);
        getStudent.Books = _mapper.Map<List<BookGetDto>>(await _bookRepo.GetByOwnerIdAsync(student.Id));

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

    public async Task<LoginResponse> Login(LoginModel model)
    {
        Student? student = await _studentRepo.GetByEmailAsync(model.Email);
        if (student == null)
            throw new InvalidOperationException($"Student with email:{model.Email} doesn't exist");

        if (!_passwordHasher.VerifyPassword(model.Password, student.Password))
            throw new InvalidOperationException($"Verify password failed");

        var token = _jwtTokenGenerator.Generate(student);
        return new LoginResponse
        {
            Token = token,
            TokenExpiryTime = DateTime.Now.AddMinutes(60)
        };
    }
}
