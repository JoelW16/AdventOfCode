using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaySeven
{
    public class ThrustBot : IIntCodeComputerBot
    {
        public int Input { get; set; }
        public int Output { get; set; }
        private List<int[]> _phasePermutations;
        
        private int _thrustOutput;
        private int _phase;
        private int _inputNumber;



        public int Run(IntCodeComputer computer, int[] program)
        {

            _phasePermutations = new List<int[]>();
            PhasePermute(new int[]{0, 1, 2, 3, 4}, 0, 4);

            var thrust = new Dictionary<int[], int>();
            foreach (var phasePermutation in _phasePermutations)
            {
                _thrustOutput = 0;
                foreach (var phase in phasePermutation)
                {
                    _inputNumber = 0;
                    _phase = phase;
                    computer.Run(program, 0, this);

                }
                thrust.Add(phasePermutation, _thrustOutput);
            }

            return thrust.Max(i => i.Value);
        }

        private static int[] Swap(int[] phases, int indexA, int indexB)
        {
            var tmp = phases[indexA];
            phases[indexA] = phases[indexB];
            phases[indexB] = tmp;
            return phases;
        }

        private void PhasePermute(int[] phases, int i, int n)
        {
            if (i == n)
            {
                var phasePerm = new int[phases.Length];
                phases.CopyTo(phasePerm, 0);
                _phasePermutations.Add(phasePerm);
            }
            else
            {
                for (var j = i; j <= n; j++)
                {
                    phases = Swap(phases, i, j);
                    PhasePermute(phases, i + 1, n);
                    phases = Swap(phases, i, j);
                }
            }
        }


        public int GetInput()
        {
            switch (_inputNumber)
            {
                case 0:
                    _inputNumber = 1;
                    return _phase;
                case 1:
                    _inputNumber = 0;
                    return _thrustOutput;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetOutput(int output)
        {
            _thrustOutput = output;
        }

    }
}
