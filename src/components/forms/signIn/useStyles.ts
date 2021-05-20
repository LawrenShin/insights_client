import {makeStyles} from "@material-ui/core";
import {loaderContainer, main} from "../../colorConstants";

export default makeStyles({
  ...loaderContainer,
  inputs: {
    display: 'flex',
    flexDirection: 'column',
    gap: '20px',
  },
  button: {
    borderRadius: '30px',
    '&:hover': {
      color: '#fff',
      background: main,
    },
  },
  textAlignRight: { textAlign: 'right', },
  fullWidth: { width: '100%' },
  whiteBack: {
    background: '#fff',
    color: main,
    border: `1px solid ${main}`,
    '&:hover': {
      background: main,
      color: 'white',
    }
  },
});