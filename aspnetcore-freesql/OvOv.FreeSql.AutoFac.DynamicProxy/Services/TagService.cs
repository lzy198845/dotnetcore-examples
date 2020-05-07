using System;
using System.Threading.Tasks;
using AutoMapper;
using OvOv.Core.Domain;
using OvOv.FreeSql.AutoFac.DynamicProxy.Repositories;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Services
{
    public class TagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [Transactional]
        public virtual async Task UpdateAsync(Tag tag)
        {
            Tag dataTag = await _tagRepository.Select.Where(r => r.Id == tag.Id).ToOneAsync();
            if (dataTag == null)
            {
                throw new Exception("该数据不存在");
            }
            bool exist = await _tagRepository.Select.AnyAsync(r => r.TagName == tag.TagName && r.Id != tag.Id);
            if (exist)
            {
                throw new Exception($"该标签：[{tag.TagName}]已存在");
            }

            dataTag.TagName = tag.TagName;

            await _tagRepository.UpdateAsync(dataTag);

            if (tag.TagName == "abc")
            {
                throw new Exception("abc Transactional");
            }

        }

        [Transactional]
        public virtual async Task CreateAsync(Tag tag)
        {
            await _tagRepository.InsertAsync(tag);

            await _tagRepository.InsertAsync(
                new Tag()
            {
                TagName = "a",
                IsDeleted = false
            }
            );
            if (tag.TagName == "abc")
            {
                throw new Exception("abc Transactional");
            }
            await _tagRepository.InsertAsync(new Tag()
            {
                TagName = "b",
                IsDeleted = false
            });
        }
    }
}