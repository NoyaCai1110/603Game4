using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCreator : MonoBehaviour
{
    [SerializeField]
    public PartyMember bard, fighter, cleric, wizard, rogue;

    //grab the template for the party member, and then copy it 
    public PartyMember CreateCharacter(string char_class, string char_name)
    {
        //temporary assignment; use the switch
        PartyMember new_member = Instantiate(bard);

        switch (char_class)
        {
            case "bard":
                {
                    new_member = Instantiate(bard);
                    break;
                }
            case "fighter":
                {
                    new_member = Instantiate(fighter);
                    break;
                }
            case "wizard":
                {
                    new_member = Instantiate(wizard);
                    break;
                }
            case "rogue":
                {
                    new_member = Instantiate(rogue);
                    break;
                }
            case "cleric":
                {
                    new_member = Instantiate(cleric);
                    break;
                }

        }

        new_member.name = char_name;

        return new_member; 
    }
}
