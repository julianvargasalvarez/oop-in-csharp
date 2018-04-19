if [ $( ls /monads/*.exe 2>/dev/null | wc -l ) -ne 0 ]; then
  rm /monads/*.exe
fi

mcs /monads/SideEffects.cs;
mono /monads/SideEffects.exe;


# sudo docker run -ti -v $(pwd):/monads mono sh /monads/runthem.sh
