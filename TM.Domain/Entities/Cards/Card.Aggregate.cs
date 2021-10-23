using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TM.Domain.Entities.CardAssigns;
using TM.Domain.Entities.CardHistories;
using TM.Domain.Entities.CardMovements;
using TM.Domain.Entities.CardTags;
using TM.Domain.Entities.Phases;
using TM.Domain.Entities.Projects;
using TM.Domain.Entities.Tags;
using TM.Domain.Entities.ToDos;
using TM.Domain.Entities.Users;
using TM.Domain.Shared;

namespace TM.Domain.Entities.Cards
{
    public partial class Card
    {
        public Card(string name) : base()
        {
            Name = name;
            PriorityId = (int)PriorityBasic.Medium;
        }

        public void AddCardToProject(Project project)
        {
            Project = project;
        }

        public void DefaultPhaseForCard()
        {
            var newCardMovement = new CardMovement((int)PhaseBasic.Opportunity);
            if (this.CardMovements != null)
                this.CardMovements.Add(newCardMovement);
            else
                this.CardMovements = new List<CardMovement>() { newCardMovement };
        }

        public void AddHistory(CardHistory cardHistory)
        {
            if (this.CardHistories != null)
                this.CardHistories.Add(cardHistory);
            else
                this.CardHistories = new List<CardHistory>() { cardHistory };
        }

        public void AddNewMovement(Phase movePhase)
        {
            var cardMovement = new CardMovement(movePhase);
            if (this.CardMovements != null)
                this.CardMovements.Add(cardMovement);
            else
                this.CardMovements = new List<CardMovement>() { cardMovement };
        }

        public void AddAssign(User user)
        {
            var newCardAssign = new CardAssign(user);
            if (this.CardAssigns != null)
                this.CardAssigns.Add(newCardAssign);
            else
                this.CardAssigns = new List<CardAssign>() { newCardAssign };
        }

        public void AddCardTag(Tag tag)
        {
            var newCardTag = new CardTag(tag);
            if (this.CardTags != null)
                this.CardTags.Add(newCardTag);
            else
                this.CardTags = new List<CardTag>() { newCardTag };
        }

        public Todo AddTodo(string name, int? parentId)
        {
            var newTodo = new Todo(name);

            if (this.Todos != null)
            {
                if (parentId != null)
                {
                    var todoParent = this.Todos.FirstOrDefault(_ => _.Id == parentId);
                    todoParent.InverseParent.Add(newTodo);
                    return newTodo;
                }

                this.Todos.Add(newTodo);
            }
            else
                this.Todos = new List<Todo>() { newTodo };

            return newTodo;
        }

        private void AddObjectToICollection<T>(ICollection<T> collection, T obj)
        {
            if (collection != null)
                collection.Add(obj);
            else
                collection = new List<T>() { obj };
        }
    }
}
