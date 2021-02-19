import React from 'react';
import { useField } from 'formik';
import { TextField } from '@material-ui/core';

export default (props: any) => {
  const { errorText, ...rest } = props;
  const [field, meta] = useField(props);

  function _renderHelperText() {
    const {touched, error} = meta;
    console.log(touched, error);

    if (touched && error) {
      return error;
    }
  }
  console.log(meta)

  return (
    <TextField
      type="text"
      error={meta.touched && meta.error}
      helperText={_renderHelperText()}
      {...field}
      {...rest}
    />
  );
}
