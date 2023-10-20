using System.Windows.Input;
using Module.MusicSourcesStorage.Gui.AbstractViewModels.WizardSteps;
using Module.MusicSourcesStorage.Gui.Entities;

namespace Module.MusicSourcesStorage.Gui.DesignTimeViewModels.WizardSteps;

public sealed class ErrorStepDTVM : IErrorStepVM
{
    public event EventHandler<StepTransitionEventArgs>? NextStepRequested;
    public event EventHandler<StepTransitionEventArgs>? PreviousStepRequested;
    public event EventHandler? CloseWizardRequested;

    public bool HasNextStep => false;
    public bool CanGoNext => false;
    public string? CustomNextStepName => null;

    public bool HasPreviousStep => true;
    public bool CanGoBack => true;

    public string Error => "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\n" +
                           "Suspendisse ullamcorper ante lorem, vitae dapibus leo egestas eu.\n" +
                           "Nulla vitae nunc dignissim, dignissim augue non, ornare neque.\n" +
                           "Donec vehicula mauris id accumsan rutrum.\n" +
                           "Donec vel risus dictum, sodales velit sed, dapibus turpis.\n" +
                           "Sed vitae ipsum nec leo tempor lacinia ac eget ante.\n" +
                           "In tincidunt euismod nibh.\n" +
                           "Integer eleifend arcu non arcu eleifend scelerisque.\n" +
                           "Ut consectetur lacus ut porta maximus.\n" +
                           "Phasellus lacinia egestas volutpat.\n" +
                           "Curabitur sed leo non libero auctor condimentum aliquet nec ante.\n" +
                           "Aenean quis tristique orci.\n" +
                           "Praesent augue ex, mollis vitae arcu non, accumsan interdum risus.\n" +
                           "Pellentesque lobortis eu lacus.\n" +
                           "Maecenas nec mollis felis, laoreet placerat nibh.\n" +
                           "Cras felis tortor, pellentesque a volutpat ut, eleifend vitae magna.\n" +
                           "Aliquam non arcu sem.\n" +
                           "Morbi dictum pharetra metus, porttitor cursus tellus elementum eget.\n" +
                           "Suspendisse pharetra metus id ipsum scelerisque malesuada.\n" +
                           "Phasellus at facilisis metus.\n" +
                           "Nunc laoreet volutpat eros, vel cursus sapien dictum at.\n" +
                           "Morbi eleifend neque arcu, sit amet volutpat nibh iaculis eu.\n" +
                           "Maecenas suscipit venenatis vestibulum.\n" +
                           "Phasellus id massa sit amet mi rhoncus feugiat.\n" +
                           "Praesent consequat mollis suscipit.\n" +
                           "Donec nec quam mauris.\n" +
                           "\n" +
                           "Morbi venenatis diam vitae lobortis tempus. Curabitur eget pharetra dolor. Sed consequat pretium tellus, id iaculis odio ullamcorper vitae. Vestibulum fringilla diam bibendum sapien consequat, in cursus enim hendrerit. Phasellus nec nunc efficitur, tristique risus vel, dictum justo. In imperdiet quis velit sit amet sollicitudin. Pellentesque tincidunt vitae odio sit amet dapibus.";

    public ICommand Back => null!;
    public ICommand Next => null!;
    public ICommand CloseWizard => null!;
}