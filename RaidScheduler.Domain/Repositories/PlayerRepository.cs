﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using RaidScheduler.Domain.DomainModels.PlayerDomain;

namespace RaidScheduler.Domain.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private readonly RaidSchedulerContext context;
        public PlayerRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public Player Find(string Id)
        {
            return context.Player.Find(Id);
        }

        public ICollection<Player> Get(Expression<Func<Player, bool>> where = null)
        {
            List<Player> result = null;
            if (where != null)
            {
                result = context.Player.Where(where).ToList();

            }
            else
            {
                result = context.Player.ToList();
            }
            return result;

        }

        public Player Save(Player player)
        {
            var cPlayer = context.Player.Where(p => p.PlayerId == player.PlayerId).SingleOrDefault();
            context.Entry<Player>(player).State = cPlayer == null ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            return player;
        }

        public void Delete(Player entity)
        {
            context.Entry<Player>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }        
    }
}