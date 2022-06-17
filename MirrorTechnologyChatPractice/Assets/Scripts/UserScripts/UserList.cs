using System.Collections.Generic;
using UnityEngine.Events;
using Mirror;

namespace NetworkChat
{

	public class UserList : NetworkBehaviour
	{
		public static UserList Instance;

		private void Awake()
		{
			Instance = this;
		}

		public List<UserData> AllUsersData = new List<UserData>();
		public static UnityAction<List<UserData>> UpdateUserList;

        public override void OnStartClient()
        {
            base.OnStartClient();

			AllUsersData.Clear();
        }

        [Server]
		public void SvAddCurrentUser(int userId, string userNickname)
		{
			UserData data = new UserData(userId, userNickname);
			AllUsersData.Add(data);

			if (isServerOnly)
			{
				RpcClearUserDataList();
			}

			for(int i = 0; i < AllUsersData.Count; ++i)
            {
				RpcAddCurrentUser(AllUsersData[i].Id, AllUsersData[i].Nickname);
            }
		}

		[Server]
		public void SvRemoveCurrentUser(int userId)
		{
			for(int i = 0; i < AllUsersData.Count; ++i)
            {
				if(AllUsersData[i].Id == userId)
                {
					AllUsersData.RemoveAt(i);
					break;
                }
            }

			RpcRemoveCurrentUser(userId);
		}

		[ClientRpc]
		private void RpcClearUserDataList()
		{
			AllUsersData.Clear();
		}

		[ClientRpc]
		private void RpcAddCurrentUser(int userId, string userNickname)
		{
			if(isClient && isServer)
            {
				UpdateUserList?.Invoke(AllUsersData);
				return;
            }

			UserData data = new UserData(userId, userNickname);
			AllUsersData.Add(data);

			UpdateUserList?.Invoke(AllUsersData);
		}

		[ClientRpc]
		private void RpcRemoveCurrentUser(int userId)
		{
			for (int i = 0; i < AllUsersData.Count; ++i)
			{
				if (AllUsersData[i].Id == userId)
				{
					AllUsersData.RemoveAt(i);
					break;
				}
			}

			UpdateUserList?.Invoke(AllUsersData);
		}
	}
}


