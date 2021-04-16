import React, {useState} from 'react';
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import Arrow from "../Arrow";
import {Rounded} from "../button";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {SignInType} from "../../pages/SignIn/duck";

const Profile = ({ logout }: any) => {
  const [open, setOpen] = useState<boolean>(false);
  const styles = useStyles();
  const moveStyle = `${open ? styles.logoutClose : styles.logoutOpen}`;

  return (<div className={styles.root}>
    <div className={`${styles.usernameContainer} ${moveStyle}`}>
      <Typography variant={'h6'}>Username</Typography>
      <div className={`${styles.arrow}`}>
        <Arrow
          onClick={() => setOpen(!open)}
          direction={open ? 'right' : 'left'}
        />
      </div>
    </div>
    <div className={`${moveStyle} ${styles.logout}`}>
      <Rounded onClick={() => logout()}>Logout</Rounded>
    </div>
  </div>)
}

export default connect(
  (state: RootState) => ({
  //  TODO: here goes name from back
  }),
  (dispatch: Dispatch) => ({
    logout: () => dispatch(CreateAction(SignInType.LOGOUT)),
  })
)(Profile);
