import {makeStyles} from "@material-ui/core";
import {paleGrey, purp} from "../colorConstants";

export default makeStyles({
  purpColor: {
    color: purp,
  },
  link: {
    '&:hover': {
      cursor: 'pointer',
      fontWeight: 'bold',
    },
  },
});
