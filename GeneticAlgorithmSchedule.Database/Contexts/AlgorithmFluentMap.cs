using GeneticAlgorithmSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeneticAlgorithmSchedule.Database.Contexts
{
    public class AlgorithmFluentMap :  IEntityTypeConfiguration<AlgorithmConfig>
    {
        public void Configure(EntityTypeBuilder<AlgorithmConfig> builder)
        {
            builder.Property(o => o.CrosoverProbability).IsRequired();
            builder.Property(o => o.IsSoftOn).IsRequired();
            builder.Property(o => o.MutationProbability).IsRequired();
            builder.Property(o => o.NumberOfChromosomes).IsRequired();
            builder.Property(o => o.MutationSize).IsRequired();
            builder.Property(o => o.TrackBest).IsRequired();
            builder.Property(o => o.NumberOfCrossoverPoints).IsRequired();
            builder.Property(o => o.SoftConditionSufficient).IsRequired();
            builder.Property(o => o.SelectionType).IsRequired();
            builder.Property(o => o.ReplaceByGeneration).IsRequired();
            builder.Property(o => o.ModifiedDate).ValueGeneratedOnAddOrUpdate();
            builder.Property(o => o.AddedDate).ValueGeneratedOnAdd();
        }
    }

    public class AlgorithmWithWeakestFluentMap : IEntityTypeConfiguration<AlgorithmWithWeakestConfig>
    {
        public void Configure(EntityTypeBuilder<AlgorithmWithWeakestConfig> builder)
        {
            builder.Property(o => o.TrackWorst).IsRequired();
        }
    }
}

