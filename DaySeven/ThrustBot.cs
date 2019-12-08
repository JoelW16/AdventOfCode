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
        private int _ampPhase;


        public int Run(string dataDirectory, bool feedbackMode = false)
        {
            _phasePermutations = new List<int[]>();
            PhasePermute(feedbackMode ? new int[] {5, 6, 7, 8, 9} : new int[] {0, 1, 2, 3, 4}, 0, 4);

            var thrust = new Dictionary<int[], int>();

            foreach (var phasePermutation in _phasePermutations)
            {
                _thrustOutput = 0;
                var result = 0;
                var amps = phasePermutation.Select(i => (new IntCodeComputer(dataDirectory, this), i)).ToList();
                if (!feedbackMode)
                {
                    RunAmps(ref amps, result);
                }
                while (feedbackMode && result == 0)
                {
                    result = RunAmps(ref amps, result);
                }

                thrust.Add(phasePermutation, _thrustOutput);
            }
            //return thrust.FirstOrDefault(f => f.Key == new int[] { 9, 8, 7, 6, 5 }).Value;
            return thrust.Max(i => i.Value);
        }

        private int RunAmps(ref List<(IntCodeComputer computer, int phase)> amps, int result)
        {
            for (var i = 0; i < amps.Count(); i++)
            {
                var computer = amps[i].computer;
                var phase = amps[i].phase;
                if (phase != -1)
                {
                    _ampPhase = phase;
                    phase = -1;
                }

                if (i < amps.Count() - 1)
                {
                    computer.Run();
                }
                else
                {
                    result = computer.Run();
                }

                amps[i] = (computer, phase);
            }
            return result;
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
            if (_ampPhase != -1)
            {
                var phase = _ampPhase;
                _ampPhase = -1;
                return phase;
            }
            else
            {
                return _thrustOutput;
            }
        }

        public void SetOutput(int output)
        {
            _thrustOutput = output;
        }

    }
}
