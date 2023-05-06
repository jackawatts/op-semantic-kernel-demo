using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel;

namespace Op.SemanticKernel.Demo.Examples.Skills
{
    internal static class ConsultantProfileSkill
    {
        private const string PromptTemplate = @"
I am a consultant who answers questions according to my experience.

I ensure my responses contain only the most relevant information to questions I am asked.

Consider only 'my experience' when writing a response to answer a question.

my experience: {{recall $query}}

Question: {{$query}}

Answer:
";
        internal static ISKFunction AddConsultantProfile(this IKernel kernel) =>
            kernel.CreateSemanticFunction(PromptTemplate, maxTokens: 256, topP: 0.2);

    }
}