﻿using System;
using BDD.Examples.TePrestamos.Web.Domain.Entities;
using BDD.Examples.TePrestamos.Web.Services;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BDD.Examples.TePrestamos.Specs.Steps
{
    [Binding]
    public class CalculoDeTasaDeInteresSteps
    {
        private readonly Customer _customer = new Customer();
        private double _calculatedInterestRate;

        [Given]
        public void Dado_que_el_cliente_es_desplazado_por_la_violencia()
        {
            _customer.IsRefugee = true;
        }
        
        [Given]
        public void Dado_que_el_cliente_es_madre_cabeza_de_hogar()
        {
            _customer.IsSingleMother = true;
        }
        
        [Given]
        public void Dado_que_el_cliente_no_es_desplazado_ni_madre_cabeza_de_hogar()
        {
            _customer.IsRefugee = false;
            _customer.IsSingleMother = false;
        }
        
        [When]
        public void Cuando_se_calcule_su_interés()
        {
            _calculatedInterestRate = LoanInterestRateCalculator.CalculateFor(_customer);
        }
        
        [Then]
        public void Entonces_deberían_descontársele_P0_puntos(int p0)
        {
            Assert.AreEqual(LoanInterestRateCalculator.DefaultInterestRate - p0, _calculatedInterestRate);
        }
        
        [Then]
        public void Entonces_el_total_no_debería_exceder_P0(int p0)
        {
            Assert.LessOrEqual(LoanInterestRateCalculator.DefaultInterestRate - _calculatedInterestRate, 10);
        }
        
        [Then]
        public void Entonces_no_deberían_realizarse_descuentos_a_su_tasa_de_interés()
        {
            Assert.AreEqual(LoanInterestRateCalculator.DefaultInterestRate, _calculatedInterestRate);
        }
    }
}
