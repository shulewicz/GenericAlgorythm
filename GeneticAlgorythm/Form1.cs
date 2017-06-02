using MomAndDadAlgorytm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorythm
{
    public partial class Form1 : Form
    {
        public static string mPath = "C:/Users/User/Desktop/net12_1.txt";

        int y = 80;
        int sizeOfPopulation = 50;
        int numberOfGenerations = 20;
        long seed = 2131421432;
        double probOfMutation = 0.9;
        double probOfCrossing = 0.9;

        public Form1()
        {
            InitializeComponent();
        }

        public void initializeData ()
        {
            mPath = textBox1.Text.ToString();
            if (mPath == "" || mPath == null)
            {
                mPath = @"C:\Users\User\Desktop\GeneticAlgorythm\net12_1.txt";
            }

            if (!int.TryParse(textBox2.Text.ToString(),out y))
            {
                    y = 100; 
            }
            
            if (!double.TryParse(textBox3.Text.ToString(), out probOfMutation))
            {
                    probOfMutation = 0.05;
            }

            if (!double.TryParse(textBox4.Text.ToString(), out probOfCrossing))
            {
                    probOfCrossing = 0.9;
            }

            if (!int.TryParse(textBox5.Text.ToString(), out sizeOfPopulation))
            {
                sizeOfPopulation = 200;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            initializeData();

            List<Chromosome> StartPopulation = new List<Chromosome>();

            Random rand = new Random();
            Parser Parser = new Parser(mPath);
            Network Network = new Network();

            Network.Link = Parser.ParseLinkPart();
            Network.Demand = Parser.ParseDemandPart();

            Population Population = new Population(Network, seed, sizeOfPopulation);

            if (radioButton1.Checked && radioButton3.Checked)
            {
                DAP Dap = new DAP(Network);
                Dap.bruteForce(Network);

                string lines = "";
                for (int i = 0; i< Dap.solution.Count(); i++)
                {
                    for (int j = 0; j< Dap.solution[i].Count(); j++)
                    {
                        lines = lines + Dap.solution[i][j].ToString() + " ";
                        
                    }

                    lines =lines + "\r\n";
                }
                File.WriteAllText("out.txt", lines);
                radioButton1.Checked = false;
                radioButton3.Checked = false;

            }

            if(radioButton1.Checked && radioButton4.Checked)
            {
                EvolutionAlgorythm Evolution = new EvolutionAlgorythm(Population.PopulationList, sizeOfPopulation, y, probOfMutation, probOfCrossing, seed, Network);
                Evolution.startEvolutionDAP();

                string lines = "";

                for (int i = 0; i < Evolution.ListOfChromosomes[0].chromosome.Count(); i++)
                {
                    for (int j = 0; j < Evolution.ListOfChromosomes[0].chromosome[i].Count(); j++)
                    {
                        lines = lines + Evolution.ListOfChromosomes[0].chromosome[i][j].ToString() + " ";

                    }

                    lines = lines + "\r\n";
                }
                File.WriteAllText("out.txt", lines);
                radioButton1.Checked = false;
                radioButton4.Checked = false;
            }

            if (radioButton2.Checked && radioButton3.Checked)
            {
                DDAP Ddap = new DDAP(Network);
                Ddap.startBruteForce(Network, y);

                string lines = "";
                lines = lines + "Best cost = " + Ddap.TheBestCost + "\r\n" + "\r\n";
                lines = lines + "Best solution = " + "\r\n" + "\r\n";

                for (int i = 0; i < Ddap.theBestSolution.Count(); i++)
                {
                    for (int j = 0; j < Ddap.theBestSolution[i].Count(); j++)
                    {
                        lines = lines + Ddap.theBestSolution[i][j].ToString() + " ";

                    }

                    lines = lines + "\r\n";
                }
                File.WriteAllText("out.txt", lines);

                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }

            if (radioButton2.Checked && radioButton4.Checked)
            {
                EvolutionAlgorythm Evolution = new EvolutionAlgorythm(Population.PopulationList, sizeOfPopulation, y, probOfMutation, probOfCrossing, seed, Network);
                Evolution.startEvolutionDDAP();

                string lines = "";
                lines = lines + "Best cost = " + Evolution.ListOfChromosomes[0].cost + "\r\n" + "\r\n";
                lines = lines + "Best solution = " + "\r\n" + "\r\n";

                for (int i = 0; i < Evolution.ListOfChromosomes[0].chromosome.Count(); i++)
                {
                    for (int j = 0; j < Evolution.ListOfChromosomes[0].chromosome[i].Count(); j++)
                    {
                        lines = lines + Evolution.ListOfChromosomes[0].chromosome[i][j].ToString() + " ";

                    }

                    lines = lines + "\r\n";
                }
                File.WriteAllText("out.txt", lines);
                radioButton2.Checked = false;
                radioButton4.Checked = false;

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
