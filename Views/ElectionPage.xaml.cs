using ElectionApp.Models;
using System.Collections.ObjectModel;

namespace ElectionApp.Views;

public partial class ElectionPage : ContentPage
{
    private ObservableCollection<CandidateDisplay> candidates = new ObservableCollection<CandidateDisplay>();
    private CandidateDisplay can { get; set; }
    private readonly Interfaces.IApiServices _services;
    private int count = 0;    
    private List<int> votes = new List<int>();
    private List<int> prevList = new List<int>();
    private List<int> curList = new List<int>();
    private Election voteSubmission = new Election();
    private Person _person = new Person();
    
    public ElectionPage(Interfaces.IApiServices services)
	{
		InitializeComponent();
		_services = services;       
        Setup();      
    }

	

   
    public async void Setup()
    {
        var can = await _services.GetCandidateDisplays();
        foreach (var c in can) 
        {
            candidates.Add(c);

        }        
        CandidateView.ItemsSource = candidates;
        if(MainPage.username > 0)
        {
            _person = MainPage.person;
        }
        else
        {
            _person = ManualMainPage.person;
        }
        
        lblElection.Text = "Candidates for Election " + DateTime.Now.Year.ToString();
        lblUser.Text = "Logged in as : " + _person.FirstName + " " + _person.LastName;

    }
  

    private void SelectCase(object sender, SelectionChangedEventArgs e)
    {
        var i = e.CurrentSelection.Count;
        var v = e.CurrentSelection.Count;
        var p = e.PreviousSelection.Count;
        var select = new CandidateDisplay();
        if(i > 0) 
        {
            i = i-1;
            select = (CandidateDisplay)e.CurrentSelection.ElementAt(i);
        }
        else
        {
            select = null;
        }
       
        
        
        if(select != null && count<=i) 
        {
            
            votes.Add(select.CandidateId);
            count++;
        }
        else
        {
            for (int counter =  0; counter < p;counter++) 
            {
                var x = (CandidateDisplay)e.PreviousSelection[counter];
                prevList.Add(x.CandidateId);
            }
            for (int counter = 0; counter < v; counter++)
            {
                var x = (CandidateDisplay)e.CurrentSelection[counter];
                curList.Add(x.CandidateId);
            }
            foreach (var c in prevList)
            {
                var y = curList.Contains(c) ;
                if(y) 
                {
                }
                else
                {
                    votes.Remove(c);                    
                }
            }
            prevList.Clear();
            curList.Clear();
            count--;
        }
        if(votes.Count > 9) 
        {
            DisplayAlert("Warning Alert", "Cannot Vote for More than 9 Candidates", "Ok");
        }
    }

    private async void vote(object sender, EventArgs e)
    {
        if (votes.Count > 9)
        {
          await  DisplayAlert("Warning Alert", "Cannot Vote for More than 9 Candidates", "Ok");
        }
        else
        {
            
            switch (votes.Count)
            {
                case 0:
                    await DisplayAlert("Warning Alert", "Cannot Submit An Empty Ballot", "Ok");                    
                    break;
                case 1:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = 0;
                    voteSubmission.Candidate3 = 0;
                    voteSubmission.Candidate4 = 0;
                    voteSubmission.Candidate5 = 0;
                    voteSubmission.Candidate6 = 0;
                    voteSubmission.Candidate7 = 0;
                    voteSubmission.Candidate8 = 0;
                    voteSubmission.Candidate9 = 0;
                    var a = await _services.Postvote(voteSubmission);
                    if(a)
                    {
                        _person.VFlag = true;
                        var a1 = await _services.LockOutVoter(_person.PersonId);
                        if (a1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }                      
                        
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }                    
                    break;
                case 2:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = 0;
                    voteSubmission.Candidate4 = 0;
                    voteSubmission.Candidate5 = 0;
                    voteSubmission.Candidate6 = 0;
                    voteSubmission.Candidate7 = 0;
                    voteSubmission.Candidate8 = 0;
                    voteSubmission.Candidate9 = 0;
                    var b = await _services.Postvote(voteSubmission);
                    if (b)
                    {
                        _person.VFlag = true;
                        var b1 = await _services.LockOutVoter(_person.PersonId);
                        if (b1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;
                case 3:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = votes.ElementAt(2);
                    voteSubmission.Candidate4 = 0;
                    voteSubmission.Candidate5 = 0;
                    voteSubmission.Candidate6 = 0;
                    voteSubmission.Candidate7 = 0;
                    voteSubmission.Candidate8 = 0;
                    voteSubmission.Candidate9 = 0;
                    var c = await _services.Postvote(voteSubmission);
                    if (c)
                    {
                        _person.VFlag = true;
                        var c1 = await _services.LockOutVoter(_person.PersonId);
                        if (c1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;
                case 4:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = votes.ElementAt(2);
                    voteSubmission.Candidate4 = votes.ElementAt(3);
                    voteSubmission.Candidate5 = 0;
                    voteSubmission.Candidate6 = 0;
                    voteSubmission.Candidate7 = 0;
                    voteSubmission.Candidate8 = 0;
                    voteSubmission.Candidate9 = 0;
                    var d = await _services.Postvote(voteSubmission);
                    if (d)
                    {
                        _person.VFlag = true;
                        var d1 = await _services.LockOutVoter(_person.PersonId);
                        if (d1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;
                case 5:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = votes.ElementAt(2);
                    voteSubmission.Candidate4 = votes.ElementAt(3);
                    voteSubmission.Candidate5 = votes.ElementAt(4);
                    voteSubmission.Candidate6 = 0;
                    voteSubmission.Candidate7 = 0;
                    voteSubmission.Candidate8 = 0;
                    voteSubmission.Candidate9 = 0;
                    var f = await _services.Postvote(voteSubmission);
                    if (f)
                    {
                        _person.VFlag = true;
                        var f1 = await _services.LockOutVoter(_person.PersonId);
                        if (f1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;
                case 6:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = votes.ElementAt(2);
                    voteSubmission.Candidate4 = votes.ElementAt(3);
                    voteSubmission.Candidate5 = votes.ElementAt(4);
                    voteSubmission.Candidate6 = votes.ElementAt(5);
                    voteSubmission.Candidate7 = 0;
                    voteSubmission.Candidate8 = 0;
                    voteSubmission.Candidate9 = 0;
                    var g = await _services.Postvote(voteSubmission);
                    if (g)
                    {
                        _person.VFlag = true;
                        var g1 = await _services.LockOutVoter(_person.PersonId);
                        if (g1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;
                case 7:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = votes.ElementAt(2);
                    voteSubmission.Candidate4 = votes.ElementAt(3);
                    voteSubmission.Candidate5 = votes.ElementAt(4);
                    voteSubmission.Candidate6 = votes.ElementAt(5);
                    voteSubmission.Candidate7 = votes.ElementAt(6);
                    voteSubmission.Candidate8 = 0;
                    voteSubmission.Candidate9 = 0;
                    var h = await _services.Postvote(voteSubmission);
                    if (h)
                    {
                        _person.VFlag = true;
                        var h1 = await _services.LockOutVoter(_person.PersonId);
                        if (h1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;
                case 8:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = votes.ElementAt(2);
                    voteSubmission.Candidate4 = votes.ElementAt(3);
                    voteSubmission.Candidate5 = votes.ElementAt(4);
                    voteSubmission.Candidate6 = votes.ElementAt(5);
                    voteSubmission.Candidate7 = votes.ElementAt(6);
                    voteSubmission.Candidate8 = votes.ElementAt(7);
                    voteSubmission.Candidate9 = 0;
                    var j = await _services.Postvote(voteSubmission);
                    if (j)
                    {
                        _person.VFlag = true;
                        var j1 = await _services.LockOutVoter(_person.PersonId);
                        if (j1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;
                case 9:
                    voteSubmission.VotersId = _person.PersonId;
                    voteSubmission.ElectionDate = DateTime.Now;
                    voteSubmission.Constituancy = 0;
                    voteSubmission.Candidate1 = votes.ElementAt(0);
                    voteSubmission.Candidate2 = votes.ElementAt(1);
                    voteSubmission.Candidate3 = votes.ElementAt(2);
                    voteSubmission.Candidate4 = votes.ElementAt(3);
                    voteSubmission.Candidate5 = votes.ElementAt(4);
                    voteSubmission.Candidate6 = votes.ElementAt(5);
                    voteSubmission.Candidate7 = votes.ElementAt(6);
                    voteSubmission.Candidate8 = votes.ElementAt(7);
                    voteSubmission.Candidate9 = votes.ElementAt(8);
                    var k = await _services.Postvote(voteSubmission);
                    if (k)
                    {
                        _person.VFlag = true;
                        var k1 = await _services.LockOutVoter(_person.PersonId);
                        if (k1)
                        {
                            await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " have successfully voted", "Ok");
                            Application.Current.Quit();
                        }
                        else
                        {
                            Application.Current.Quit();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Confirmation", _person.FirstName.Trim() + " " + _person.LastName.Trim() + " vote unsuccesful", "Ok");
                    }
                    break;                    
            }
            
        }
    }

    private void exit(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
}