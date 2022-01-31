using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using CustomParticles.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CustomParticles.UI
{
	internal class ParticleListViewController : BSMLResourceViewController
	{
		public override string ResourceName => "CustomParticles.UI.Views.ParticleList.bsml";

		private PluginConfig config;

		//public Action<Texture> customParticleChanged;
		//public Action customParticlesReloaded;

		[Inject]
		public void Construct(PluginConfig config)
		{
			this.config = config;
		}

		//[UIAction("#post-parse")]
		//public void SetupList()
		//{
			//customListTableData.data.Clear();

			//foreach (CustomNote note in _noteAssetLoader.CustomNoteObjects)
			//{
			//	Sprite sprite = Sprite.Create(note.Descriptor.Icon, new Rect(0, 0, note.Descriptor.Icon.width, note.Descriptor.Icon.height), new Vector2(0.5f, 0.5f));
			//	CustomListTableData.CustomCellInfo customCellInfo = new CustomListTableData.CustomCellInfo(note.Descriptor.NoteName, note.Descriptor.AuthorName, sprite);
			//	customListTableData.data.Add(customCellInfo);
			//}

			//customListTableData.tableView.ReloadData();
			//int selectedNote = _noteAssetLoader.SelectedNote;

			//customListTableData.tableView.ScrollToCellWithIdx(selectedNote, TableView.ScrollPositionType.Beginning, false);
			//customListTableData.tableView.SelectCellWithIdx(selectedNote);
		//}
	}
}
